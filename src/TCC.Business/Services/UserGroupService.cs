using TCC.Business.Interfaces;
using TCC.Business.Models;

namespace TCC.Business.Services
{
    public class UserGroupService(INotifier notifier, IUserGroupRepository userGroupRepository) : BaseService(notifier), IUserGroupService
    {
        public async Task Add(UserGroup userGroup)
        {
            var userGroups = await userGroupRepository.GetByUserAndGroup(userGroup.UserId, userGroup.GroupId);

            if (userGroups.Any())
            {
                Notify("Usuário já foi adicionado a esse grupo");
                return;
            }

            await userGroupRepository.Add(userGroup);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
