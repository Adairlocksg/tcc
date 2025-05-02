using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IGroupService : IDisposable
    {
        Task Add(Group group);
        Task Update(Group group);
        Task AddCategory(Group group, Category category);
        Task UpdateCategory(Group group, Category category);
    }
}
