using TCC.Application.Dtos;
using TCC.Application.Views;
using TCC.Business.Base;
using TCC.Business.Models;

namespace TCC.Application.Services.Groups
{
    public interface IGroupAppService : IDisposable
    {
        Task<Result<IdView>> Add(GroupDto dto);
        Task<Result<string>> GenerateLink(Guid groupId);
        Task<Result<CategoryView>> AddCategory(Guid groupId, CategoryDto dto);
        Task<Result<IEnumerable<CategoryView>>> GetCategories(Guid groupId);
        Task<Result<IEnumerable<GroupView>>> GetAll();
        Task<Result<IdView>> Update(Guid id, GroupDto dto);
        Task<Result<GroupView>> GetById(Guid id);
        Task<Result<CategoryView>> UpdateCategory(Guid groupId, Guid categoryId, CategoryDto dto);
        Task<Result<IdView>> Favorite(Guid id);
        Task<Result<IdView>> Unfavorite(Guid id);
        Task<Result<IEnumerable<GroupMemberView>>> GetMembers(Guid id);
    }
}
