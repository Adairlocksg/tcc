using Microsoft.EntityFrameworkCore;
using TCC.Application.Views;
using TCC.Business.Base;
using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Infra.Helpers;

namespace TCC.Application.Services.Dashboards
{
    public class DashboardAppService(IUserGroupRepository userGroupRepository,
                                     ITokenHelper tokenHelper,
                                     IExpenseRepository expenseRepository,
                                     IExpenseService expenseService) : IDashboardAppService
    {
        public async Task<Result<MainDashboardView>> GetMainDashboard()
        {
            var userId = tokenHelper.GetUserIdFromClaim();

            var userGroups = await userGroupRepository.GetByUser(userId);

            var now = DateTime.UtcNow;

            var currentMonthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var nextMonthStart = currentMonthStart.AddMonths(1);

            var previousMonthStart = currentMonthStart.AddMonths(-1);
            var previousMonthEnd = currentMonthStart;

            List<GroupDashboardView> groupResults = [];

            foreach (var userGroup in userGroups)
            {
                var currentExpenses = expenseRepository.GetByGroupAndDateRange(userGroup.GroupId, currentMonthStart, nextMonthStart);
                var currentTotal = await SumExpenses(currentExpenses, currentMonthStart, nextMonthStart);

                var previousExpenses = expenseRepository.GetByGroupAndDateRange(userGroup.GroupId, previousMonthStart, previousMonthEnd);
                var previousTotal = await SumExpenses(previousExpenses, previousMonthStart, previousMonthEnd);

                decimal percentageChange = 0;

                if (previousTotal != 0)
                {
                    percentageChange = ((currentTotal - previousTotal) / previousTotal) * 100;
                }

                var userGroupsByGroup = await userGroupRepository.GetByGroups([userGroup.GroupId]);

                var groupDashboard = new GroupDashboardView
                {
                    GroupId = userGroup.GroupId,
                    Name = userGroup.Group.Name,
                    Description = userGroup.Group.Description,
                    MembersCount = userGroupsByGroup.Count(),
                    TotalCurrentMonth = currentTotal,
                    TotalPreviousMonth = previousTotal,
                    PercentageChange = percentageChange,
                    Favorite = userGroup.Favorite,
                    Admin = userGroup.Admin
                };

                groupResults.Add(groupDashboard);
            }

            var totalCurrent = groupResults.Sum(g => g.TotalCurrentMonth);
            var totalPrevious = groupResults.Sum(g => g.TotalPreviousMonth);

            decimal totalPercentageChange = 0;
            if (totalPrevious != 0)
            {
                totalPercentageChange = ((totalCurrent - totalPrevious) / totalPrevious) * 100;
            }

            var dashboard = new MainDashboardView
            {
                TotalCurrentMonth = totalCurrent,
                TotalPreviousMonth = totalPrevious,
                PercentageChange = totalPercentageChange,
                Groups = [.. groupResults.OrderByDescending(x => x.Favorite)]
            };

            return Result.Success(dashboard);
        }

        private async Task<decimal> SumExpenses(IQueryable<Expense> expensesQuery, DateTime startDate, DateTime endDate)
        {
            var expenses = await expensesQuery.ToListAsync();
            decimal total = 0;

            foreach (var expense in expenses)
            {
                var occurrences = expenseService.CalculateOcurrencesByDateRange(expense, startDate, endDate);
                total += occurrences * expense.Value;
            }

            return total;
        }

    }
}
