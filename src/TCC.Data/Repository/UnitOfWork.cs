using TCC.Business.Interfaces;
using TCC.Data.Context;

namespace TCC.Data.Repository
{
    public class UnitOfWork(MyDbContext context) : IUnityOfWork
    {
        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }
    }
}
