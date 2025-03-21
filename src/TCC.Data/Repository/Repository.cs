using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Data.Context;

namespace TCC.Data.Repository
{
    public abstract class Repository<TEntity>(MyDbContext db) : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MyDbContext Db = db;
        protected readonly DbSet<TEntity> DbSet = db.Set<TEntity>();

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual async Task Remove(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
