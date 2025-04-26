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
                                   ICategoryRepository categoryRepository) : IExpenseAppService
    {
        public async Task<Result<IdView>> Add(ExpenseDto dto)
        {
            var category = await categoryRepository.GetById(dto.CategoryId);
            if (category is null)
                return Result.Failure<IdView>(new Error("404", $"Categoria de código {dto.CategoryId} não encontrado"));

            var userGroupFromClaim = await userGroupRepository.GetByUserAndGroup(tokenHelper.GetUserIdFromClaim(), dto.GroupId);
            if (userGroupFromClaim is null)
                return Result.Failure<IdView>(new Error("404", $"Usuário de código {tokenHelper.GetUserIdFromClaim()} não encontrado no grupo de código {dto.GroupId}"));

            var userGroup = userGroupFromClaim;

            if (userGroupFromClaim.UserId != dto.UserId)
            {
                if (!userGroupFromClaim.Admin)
                    return Result.Failure<IdView>(new Error("403", $"Usuário {userGroupFromClaim.User.UserName} não tem permissão para adicionar despesas para outras pessoas no grupo {userGroupFromClaim.Group.Description}"));

                userGroup = await userGroupRepository.GetByUserAndGroup(dto.UserId, dto.GroupId);
            }

            var expense = new Expense(dto.Description,
                                      dto.Value,
                                      dto.BeginDate,
                                      dto.EndDate,
                                      dto.Recurrence,
                                      dto.RecurrenceInterval,
                                      dto.IsRecurring,
                                      userGroup.User,
                                      category,
                                      userGroup.Group);

            await expenseService.Add(expense);

            if (notifier.HasNotification())
                return Result.Failure<IdView>(new Error("400", notifier.GetNotificationMessage()));

            return Result.Success(new IdView(expense.Id));
        }

        public async Task<Result<List<ExpenseView>>> GetByGroupAndDateRange(Guid groupId, DateTime startDate, DateTime endDate)
        {
            var group = await userGroupRepository.GetByUserAndGroup(tokenHelper.GetUserIdFromClaim(), groupId);
            if (group is null)
                return Result.Failure<List<ExpenseView>>(new Error("404", $"Grupo de código {groupId} não encontrado, ou usuário logado não pertence ao grupo"));

            var expenses = await expenseRepository.GetByGroupAndDateRange(groupId, startDate, endDate);

            var result = new List<ExpenseView>();

            foreach (var expense in expenses)
            {
                if (!expense.IsRecurring)
                {
                    if (expense.BeginDate >= startDate && expense.BeginDate <= endDate)
                    {
                        result.Add(MapToView(expense, expense.BeginDate));
                    }
                }
                else
                {
                    var currentDate = expense.BeginDate;

                    while (currentDate <= endDate)
                    {
                        if (currentDate >= startDate)
                        {
                            result.Add(MapToView(expense, currentDate));
                        }

                        currentDate = GetNextOccurrenceDate(currentDate, expense);

                        if (expense.EndDate.HasValue && currentDate > expense.EndDate.Value)
                            break;
                    }
                }
            }

            return Result.Success(result);
        }

        private static ExpenseView MapToView(Expense expense, DateTime occurrenceDate)
        {
            return new ExpenseView
            {
                Description = expense.Description,
                Value = expense.Value,
                BeginDate = occurrenceDate,
                EndDate = null, // Para instância virtual, pode ser nulo ou copiar o EndDate original dependendo da regra
                IsRecurring = expense.IsRecurring,
                Recurrence = expense.Recurrence,
                RecurrenceInterval = expense.RecurrenceInterval,
                Active = expense.Active,
                UserId = expense.UserId,
                CategoryId = expense.CategoryId,
                GroupId = expense.GroupId
            };
        }

        private static DateTime GetNextOccurrenceDate(DateTime current, Expense expense)
        {
            return expense.Recurrence switch
            {
                RecurrenceType.Daily => current.AddDays(1),
                RecurrenceType.Weekly => current.AddDays(7),
                RecurrenceType.Monthly => current.AddMonths(1),
                RecurrenceType.Custom => current.AddDays(expense.RecurrenceInterval),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
