using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IGroupService : IDisposable
    {
        Task Add(Group group);
        Task Update(Group group);
        Task Remove(Guid id);
    }
}
