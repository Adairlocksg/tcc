using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Business.Models.Validations;

namespace TCC.Business.Services
{
    public class GroupService(INotifier notifier, IGroupRepository groupRepository) : BaseService(notifier), IGroupService
    {
        public async Task Add(Group group)
        {
            if (!ExecuteValidation(new GroupValidation(), group))
                return;

            await groupRepository.Add(group);
        }

        public async Task Update(Group group)
        {
            if (!ExecuteValidation(new GroupValidation(), group))
                return;

            await groupRepository.Update(group);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
