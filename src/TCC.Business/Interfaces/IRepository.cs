using System.Linq.Expressions;
using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(Guid id);
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);
    }
}
