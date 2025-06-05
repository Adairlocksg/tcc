using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Business.Models.Validations;

namespace TCC.Business.Services
{
    public class ExpenseService(INotifier notifier, IExpenseRepository expenseRepository) : BaseService(notifier), IExpenseService
    {
        public async Task Add(Expense expense)
        {
            if (!ExecuteValidation(new ExpenseValidation(), expense))
                return;

            await expenseRepository.Add(expense);
        }

        public async Task Update(Expense expense)
        {
            if (!ExecuteValidation(new ExpenseValidation(), expense))
                return;

            await expenseRepository.Update(expense);
        }

        public async Task Remove(Expense expense)
        {
            await expenseRepository.Remove(expense.Id);
        }

        public int CalculateOcurrencesByDateRange(Expense expense, DateTime start, DateTime end)
        {
            start = start.Date;
            end = end.Date;

            if (!expense.IsRecurring)
            {
                return (expense.BeginDate >= start && expense.BeginDate <= end) ? 1 : 0;
            }

            var effectiveStart = expense.BeginDate > start ? expense.BeginDate : start;
            var effectiveEnd = expense.EndDate.HasValue && expense.EndDate.Value < end ? expense.EndDate.Value : end;

            int occurrences = 0;
            switch (expense.Recurrence)
            {
                case RecurrenceType.Daily:
                    occurrences = (int)(effectiveEnd.Date - effectiveStart.Date).TotalDays + 1;
                    break;

                case RecurrenceType.Weekly:
                    occurrences = ((int)(effectiveEnd.Date - effectiveStart.Date).TotalDays / 7) + 1;
                    break;

                case RecurrenceType.Monthly:
                    occurrences = ((effectiveEnd.Year - effectiveStart.Year) * 12 + effectiveEnd.Month - effectiveStart.Month) + 1;
                    break;

                case RecurrenceType.Custom:
                    if (expense.RecurrenceInterval > 0)
                    {
                        occurrences = ((int)(effectiveEnd.Date - effectiveStart.Date).TotalDays / expense.RecurrenceInterval) + 1;
                    }
                    break;
            }

            return occurrences;
        }
    }
}
