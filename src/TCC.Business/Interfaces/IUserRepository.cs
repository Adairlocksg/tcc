using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByUsername(string userName);
    }
}
