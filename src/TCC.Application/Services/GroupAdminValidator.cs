using TCC.Business.Base;
using TCC.Business.Interfaces;
using TCC.Infra.Helpers;

namespace TCC.Application.Services
{
    public class GroupAdminValidator(IUserGroupRepository userGroupRepository, 
                                     ITokenHelper tokenHelper): IGroupAdminValidator
    {
        public async Task<Result> Validate(Guid groupId)
        {
            var userGroup = await userGroupRepository.GetByUserAndGroup(tokenHelper.GetUserIdFromClaim(), groupId);
            
            if (userGroup is null)
                return Result.Failure(new Error("403", "Usuário não pertence a esse grupo"));
            
            if (!userGroup.Admin)
                return Result.Failure(new Error("403", "Usuário não é administrador do grupo"));

            return Result.Success();
        }
    }
}
