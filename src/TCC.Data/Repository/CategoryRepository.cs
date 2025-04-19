using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Data.Context;

namespace TCC.Data.Repository
{
    public class CategoryRepository(MyDbContext db) : Repository<Category>(db), ICategoryRepository
    {
    }
}
