using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        Task<IEnumerable<Expense>> GetByGroupAndDateRange(Guid groupId, DateTime startDate, DateTime endDate);
    }
}
