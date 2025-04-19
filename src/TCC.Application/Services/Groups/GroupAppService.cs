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
                                 IUserGroupService userGroupService,
                                 ITokenHelper tokenHelper,
                                 INotifier notifier,
                                 IMapper mapper,
                                 IUnityOfWork unityOfWork) : IGroupAppService
    {
        public async Task<Result<GroupView>> Add(GroupDto dto)
        {
            await unityOfWork.BeginTransactionAsync();

            var group = new Group(dto.Description);

            await groupService.Add(group);

            if (notifier.HasNotification())
            {
                await unityOfWork.RollbackTransactionAsync();
                return Result.Failure<GroupView>(new Error("400", notifier.GetNotificationMessage()));
            }

            var userId = tokenHelper.GetUserIdFromClaim();

            var userGroup = new UserGroup(userId, group.Id, true, false);

            await userGroupService.Add(userGroup);

            if (notifier.HasNotification())
            {
                await unityOfWork.RollbackTransactionAsync();   
                return Result.Failure<GroupView>(new Error("400", notifier.GetNotificationMessage()));
            }

            await unityOfWork.CommitTransactionAsync();

            return Result.Success(mapper.Map<GroupView>(group));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
