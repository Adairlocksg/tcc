using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task Add(User user);
        Task Update(User user);
    }
}
