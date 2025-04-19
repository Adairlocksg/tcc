using TCC.Business.Base;

namespace TCC.Application.Services
{
    public interface IGroupAdminValidator
    {
        Task<Result> Validate(Guid groupId);
    }
}
