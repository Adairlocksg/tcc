using Microsoft.EntityFrameworkCore;
using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Data.Context;

namespace TCC.Data.Repository
{
    public class ExpenseRepository(MyDbContext db) : Repository<Expense>(db), IExpenseRepository
    {
        public IQueryable<Expense> GetByGroupAndDateRange(Guid groupId, DateTime startDate, DateTime endDate)
        {
            return DbSet.AsNoTracking()
                 .Include(e => e.User)
                 .Include(e => e.Category)
                 .Where(e => e.BeginDate <= endDate && (e.EndDate == null || e.EndDate >= startDate) && e.GroupId == groupId);
        }
    }
}
