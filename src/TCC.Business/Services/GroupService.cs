using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Business.Models.Validations;

namespace TCC.Business.Services
{
    public class GroupService(INotifier notifier, IGroupRepository groupRepository, ICategoryRepository categoryRepository) : BaseService(notifier), IGroupService
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

        public async Task AddCategory(Group group, Category category)
        {
            if (group.Categories.Any(c => c.Description == category.Description))
            {
                Notify($"Já existe uma categoria com a mesma descrição no grupo {group.Description}");
                return;
            }

            if (!ExecuteValidation(new CategoryValidation(), category))
                return;

            await categoryRepository.Add(category);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
