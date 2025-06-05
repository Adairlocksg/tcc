using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IExpenseService
    {
        Task Add(Expense expense);
        Task Remove(Expense expense);
        int CalculateOcurrencesByDateRange(Expense expense, DateTime startDate, DateTime endDate);
        Task Update(Expense expense);
    }
}
