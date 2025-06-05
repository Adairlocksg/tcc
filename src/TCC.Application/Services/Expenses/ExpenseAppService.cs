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

        public async Task<Result<IdView>> Update(Guid id, ExpenseDto dto)
        {
            var expense = await expenseRepository.GetById(id);
            if (expense is null)
                return Result.Failure<IdView>(new Error("404", $"Despesa de código {id} não encontrada"));

            var userGroup = await userGroupRepository.GetByUserAndGroup(tokenHelper.GetUserIdFromClaim(), expense.GroupId);
            if (userGroup is null || (!userGroup.Admin && userGroup.UserId != expense.UserId))
                return Result.Failure<IdView>(new Error("403", $"Usuário não tem permissão para editar a despesa de código {id}"));

            var category = await categoryRepository.GetById(dto.CategoryId);
            if (category is null)
                return Result.Failure<IdView>(new Error("404", $"Categoria de código {dto.CategoryId} não encontrada"));

            dto.UserId ??= tokenHelper.GetUserIdFromClaim();

            UserGroup userGroupToUpdate = null;

            if (userGroup.UserId != dto.UserId)
            {
                if (!userGroup.Admin)
                    return Result.Failure<IdView>(new Error("403", $"Usuário {userGroup.User.UserName} não tem permissão para editar despesas de outras pessoas no grupo {userGroup.Group.Description}"));

                userGroupToUpdate = await userGroupRepository.GetByUserAndGroup((Guid)dto.UserId, expense.GroupId);
                if (userGroupToUpdate is null)
                    return Result.Failure<IdView>(new Error("404", $"Usuário de código {dto.UserId} não encontrado no grupo de código {expense.GroupId}"));
            }

            expense.Update(dto.Description,
                           dto.Value,
                           dto.BeginDate,
                           dto.EndDate,
                           dto.Recurrence,
                           dto.RecurrenceInterval,
                           dto.IsRecurring,
                           userGroup.UserId == dto.UserId ? userGroup.UserId : userGroupToUpdate.UserId,
                           category.Id);

            await expenseService.Update(expense);

            if (notifier.HasNotification())
                return Result.Failure<IdView>(new Error("400", notifier.GetNotificationMessage()));

            await unityOfWork.CommitAsync();

            return Result.Success(new IdView(expense.Id));

        }

        public async Task<Result<ExpenseView>> GetById(Guid id)
        {
            var expense = await expenseRepository.GetById(id);
            if (expense is null)
                return Result.Failure<ExpenseView>(new Error("404", $"Despesa de código {id} não encontrada"));

            var userGroup = await userGroupRepository.GetByUserAndGroup(tokenHelper.GetUserIdFromClaim(), expense.GroupId);
            if (userGroup is null)
                return Result.Failure<ExpenseView>(new Error("403", $"Usuário não tem permissão para visualizar a despesa de código {id}"));

            var view = new ExpenseView
            {
                Id = expense.Id,
                Description = expense.Description,
                Value = expense.Value,
                BeginDate = expense.BeginDate,
                EndDate = expense.EndDate,
                Recurrence = expense.Recurrence,
                RecurrenceInterval = expense.RecurrenceInterval,
                IsRecurring = expense.IsRecurring,
                CategoryId = expense.Category.Id
            };

            return Result.Success(view);
        }

        public async Task<Result<IdView>> Remove(Guid id)
        {
            var expense = await expenseRepository.GetById(id);
            if (expense is null)
                return Result.Failure<IdView>(new Error("404", $"Despesa de código {id} não encontrada"));

            var userGroup = await userGroupRepository.GetByUserAndGroup(tokenHelper.GetUserIdFromClaim(), expense.GroupId);
            if (userGroup is null || (!userGroup.Admin && userGroup.UserId != expense.UserId))
                return Result.Failure<IdView>(new Error("403", $"Usuário não tem permissão para remover a despesa de código {id}"));

            await expenseService.Remove(expense);

            if (notifier.HasNotification())
                return Result.Failure<IdView>(new Error("400", notifier.GetNotificationMessage()));

            await unityOfWork.CommitAsync();
            return Result.Success(new IdView(expense.Id));
        }

        public async Task<Result<List<ExpenseSummaryView>>> GetExpenseSummaryByGroup(GetExpenseSummaryByGroupDto dto)
        {
            if (dto.EndDate < dto.StartDate)
                return Result.Failure<List<ExpenseSummaryView>>(new Error("400", "A data final não pode ser anterior à data inicial."));

            var group = await userGroupRepository.GetByUserAndGroup(tokenHelper.GetUserIdFromClaim(), dto.GroupId);
            if (group is null)
                return Result.Failure<List<ExpenseSummaryView>>(new Error("404", $"Grupo de código {dto.GroupId} não encontrado, ou usuário logado não pertence ao grupo"));

            var query = expenseRepository.GetByGroupAndDateRange(dto.GroupId, dto.StartDate, dto.EndDate);

            if (dto.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == dto.CategoryId.Value);
            }

            var summaryList = new List<ExpenseSummaryView>();

            foreach (var expense in query.ToList())
            {
                var occurrences = expenseService.CalculateOcurrencesByDateRange(expense, dto.StartDate, dto.EndDate);

                var totalValue = occurrences * expense.Value;

                if (totalValue == 0)
                    continue;

                summaryList.Add(new ExpenseSummaryView
                {
                    Id = expense.Id,
                    Description = expense.Description,
                    TotalValue = totalValue,
                    RecurrenceType = expense.Recurrence,
                    IsRecurring = expense.IsRecurring,
                    RecurrencyInterval = expense.RecurrenceInterval,
                    UserName = expense.User.UserName,
                    CategoryName = expense.Category.Description,
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
