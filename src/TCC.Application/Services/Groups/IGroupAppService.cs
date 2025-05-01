using TCC.Application.Dtos;
using TCC.Application.Views;
using TCC.Business.Base;

namespace TCC.Application.Services.Groups
{
    public interface IGroupAppService : IDisposable
    {
        Task<Result<IdView>> Add(GroupDto dto);
        Task<Result<string>> GenerateLink(Guid groupId);
        Task<Result<CategoryView>> AddCategory(Guid groupId, CategoryDto dto);
        Task<Result<IEnumerable<CategoryView>>> GetCategories(Guid groupId);
        Task<Result<IEnumerable<GroupView>>> GetAll();
    }
}
