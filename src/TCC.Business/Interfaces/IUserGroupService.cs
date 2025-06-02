using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IUserGroupService : IDisposable
    {
        Task Add(UserGroup userGroup);
        Task Favorite(UserGroup userGroup);
        Task Unfavorite(UserGroup userGroup);
    }
}
