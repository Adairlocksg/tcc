using AutoMapper;
using TCC.Application.Dtos;
using TCC.Application.Views;
using TCC.Business.Base;
using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Infra.Helpers;

namespace TCC.Application.Services.Groups
{
    public class GroupAppService(IGroupService groupService,
                                 IGroupRepository groupRepository,
                                 IUserGroupRepository userGroupRepository,
                                 IUserGroupService userGroupService,
                                 ITokenHelper tokenHelper,
                                 INotifier notifier,
                                 IMapper mapper,
                                 IGroupAdminValidator groupAdminValidator,
                                 IUnityOfWork unityOfWork) : IGroupAppService
    {
        public async Task<Result<IdView>> Add(GroupDto dto)
        {
            await unityOfWork.BeginTransactionAsync();

            var group = new Group(dto.Name, dto.Description);

            await groupService.Add(group);

            if (notifier.HasNotification())
            {
                await unityOfWork.RollbackTransactionAsync();
                return Result.Failure<IdView>(new Error("400", notifier.GetNotificationMessage()));
            }

            var userGroup = new UserGroup(tokenHelper.GetUserIdFromClaim(), group.Id, true, false);

            await userGroupService.Add(userGroup);

            if (notifier.HasNotification())
            {
                await unityOfWork.RollbackTransactionAsync();
                return Result.Failure<IdView>(new Error("400", notifier.GetNotificationMessage()));
            }

            await unityOfWork.CommitTransactionAsync();

            return Result.Success(mapper.Map<IdView>(group));
        }

        public async Task<Result<string>> GenerateLink(Guid id)
        {
            var group = await groupRepository.GetById(id);
            if (group is null)
                return Result.Failure<string>(new Error("404", $"Grupo de código {id} não encontrado"));

            var validationGroupAdmin = await groupAdminValidator.Validate(id);
            if (!validationGroupAdmin.IsSuccess)
                return Result.Failure<string>(new Error(validationGroupAdmin.Error.Code, validationGroupAdmin.Error.Message));

            return Result.Success($"sharethebill-invitation/{id}");
        }

        public async Task<Result<CategoryView>> AddCategory(Guid id, CategoryDto dto)
        {
            var group = await groupRepository.GetWithCategories(id);

            if (group is null)
                return Result.Failure<CategoryView>(new Error("404", $"Grupo de código {id} não encontrado"));

            var userGroup = await userGroupRepository.GetByUserAndGroup(tokenHelper.GetUserIdFromClaim(), id);
            if (userGroup is null)
                return Result.Failure<CategoryView>(new Error("403", "Usuário precisa pertencer ao grupo para poder criar uma categoria"));

            var category = new Category(dto.Description, id);

            await groupService.AddCategory(group, category);

            if (notifier.HasNotification())
                return Result.Failure<CategoryView>(new Error("400", notifier.GetNotificationMessage()));

            await unityOfWork.CommitAsync();

            return Result.Success(mapper.Map<CategoryView>(category));
        }

        public async Task<Result<IEnumerable<CategoryView>>> GetCategories(Guid id)
        {
            var group = await groupRepository.GetWithCategories(id);
            if (group is null)
                return Result.Failure<IEnumerable<CategoryView>>(new Error("404", $"Grupo de código {id} não encontrado"));

            var userGroup = await userGroupRepository.GetByUserAndGroup(tokenHelper.GetUserIdFromClaim(), id);
            if (userGroup is null)
                return Result.Failure<IEnumerable<CategoryView>>(new Error("403", "Usuário precisa pertencer ao grupo para poder listar categorias"));

            return Result.Success(group.Categories.Select(c => mapper.Map<CategoryView>(c)));
        }

        public async Task<Result<IEnumerable<GroupView>>> GetAll()
        {
            var userId = tokenHelper.GetUserIdFromClaim();
            var userGroups = await userGroupRepository.GetByUser(userId);
            var ret = userGroups.Select(ug => new GroupView
            {
                Id = ug.GroupId,
                Name = ug.Group.Name,
                Description = ug.Group.Description,
                Members = ug.Group.UserGroups.Count(),
                Favorite = ug.Favorite,
                Active = ug.Group.Active,
                Admin = ug.Admin,
            });

            return Result.Success(ret);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
