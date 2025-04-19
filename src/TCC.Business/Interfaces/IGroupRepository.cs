using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<Group> GetWithCategories(Guid id);
        Task<IEnumerable<Category>> GetCategories(Guid groupId);
    }
}
