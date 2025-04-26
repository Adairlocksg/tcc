using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IExpenseService
    {
        Task Add(Expense expense);
        Task Remove(Expense expense);
    }
}
