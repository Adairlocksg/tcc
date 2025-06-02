using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        IQueryable<Expense> GetByGroupAndDateRange(Guid groupId, DateTime startDate, DateTime endDate);
    }
}
