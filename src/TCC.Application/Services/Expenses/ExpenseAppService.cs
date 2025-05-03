using TCC.Application.Dtos;
using TCC.Application.Views;
using TCC.Business.Base;
using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Infra.Helpers;

namespace TCC.Application.Services.Expenses
{
    public class ExpenseAppService(IExpenseService expenseService,
                                   IExpenseRepository expenseRepository,
                                   IUserGroupRepository userGroupRepository,
                                   ITokenHelper tokenHelper,
                                   INotifier notifier,
                                   ICategoryRepository categoryRepository,
                                   IUnityOfWork unityOfWork) : IExpenseAppService
    {
        public async Task<Result<IdView>> Add(ExpenseDto dto)
        {
            var category = await categoryRepository.GetById(dto.CategoryId);
            if (category is null)
                return Result.Failure<IdView>(new Error("404", $"Categoria de código {dto.CategoryId} não encontrado"));

            var userGroupFromClaim = await userGroupRepository.GetByUserAndGroup(tokenHelper.GetUserIdFromClaim(), dto.GroupId);
            if (userGroupFromClaim is null)
                return Result.Failure<IdView>(new Error("404", $"Usuário de código {tokenHelper.GetUserIdFromClaim()} não encontrado no grupo de código {dto.GroupId}"));

            dto.UserId ??= tokenHelper.GetUserIdFromClaim();

            UserGroup userGroup = null;

            if (userGroupFromClaim.UserId != dto.UserId)
            {
                if (!userGroupFromClaim.Admin)
                    return Result.Failure<IdView>(new Error("403", $"Usuário {userGroupFromClaim.User.UserName} não tem permissão para adicionar despesas para outras pessoas no grupo {userGroupFromClaim.Group.Description}"));

                userGroup = await userGroupRepository.GetByUserAndGroup((Guid)dto.UserId, dto.GroupId);
                if (userGroup is null)
                    return Result.Failure<IdView>(new Error("404", $"Usuário de código {dto.UserId} não encontrado no grupo de código {dto.GroupId}"));
            }

            var expense = new Expense(dto.Description,
                                      dto.Value,
                                      dto.BeginDate,
                                      dto.EndDate,
                                      dto.Recurrence,
                                      dto.RecurrenceInterval,
                                      dto.IsRecurring,
                                      userGroupFromClaim.UserId == dto.UserId ? userGroupFromClaim.UserId : userGroup.UserId,
                                      category.Id,
                                      userGroupFromClaim.UserId == dto.UserId ? userGroupFromClaim.GroupId : userGroup.GroupId);

            await expenseService.Add(expense);

            if (notifier.HasNotification())
                return Result.Failure<IdView>(new Error("400", notifier.GetNotificationMessage()));

            await unityOfWork.CommitAsync();

            return Result.Success(new IdView(expense.Id));
        }

        public async Task<Result<List<ExpenseSummaryView>>> GetExpenseSummaryByGroup(GetExpenseSummaryByGroupDto dto)
        {
            var group = await userGroupRepository.GetByUserAndGroup(tokenHelper.GetUserIdFromClaim(), dto.GroupId);
            if (group is null)
                return Result.Failure<List<ExpenseSummaryView>>(new Error("404", $"Grupo de código {dto.GroupId} não encontrado, ou usuário logado não pertence ao grupo"));

            var expenses = await expenseRepository.GetByGroupAndDateRange(dto.GroupId, dto.StartDate, dto.EndDate);

            var summaryList = new List<ExpenseSummaryView>();

            foreach (var expense in expenses)
            {
                var occurrences = expenseService.CalculateOcurrencesByDateRange(expense, dto.StartDate, dto.EndDate);

                var totalValue = occurrences * expense.Value;

                summaryList.Add(new ExpenseSummaryView
                {
                    Id = expense.Id,
                    Description = expense.Description,
                    TotalValue = totalValue,
                    RecurrenceType = expense.Recurrence,
                    IsRecurring = expense.IsRecurring
                });
            }

            return Result.Success(summaryList);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
