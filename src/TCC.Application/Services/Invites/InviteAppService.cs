using TCC.Application.Dtos;
using TCC.Application.Views;
using TCC.Business.Base;
using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Business.Services;
using TCC.Infra.Helpers;

namespace TCC.Application.Services.Invites
{
    public class InviteAppService(IGroupRepository groupRepository,
                                  IInviteService inviteService,
                                  ITokenHelper tokenHelper,
                                  INotifier notifier,
                                  IInviteRepository inviteRepository,
                                  IUserGroupService userGroupService,
                                  IGroupAdminValidator groupAdminValidator,
                                  IUnityOfWork unityOfWork) : IInviteAppService
    {
        public async Task<Result<IdView>> Add(InviteDto dto)
        {
            var group = await groupRepository.GetById(dto.GroupId);
            if (group is null)
                return Result.Failure<IdView>(new Error("404", $"Grupo de código {dto.GroupId} não encontrado"));

            var userId = tokenHelper.GetUserIdFromClaim();
            var invite = new Invite(userId, dto.GroupId);

            await inviteService.Add(invite);

            if (notifier.HasNotification())
                return Result.Failure<IdView>(new Error("400", notifier.GetNotificationMessage()));

            await unityOfWork.CommitAsync();

            return Result.Success(new IdView(invite.Id));
        }

        public async Task<Result<IEnumerable<InviteView>>> GetPendingInvitesForAdmin()
        {
            var userId = tokenHelper.GetUserIdFromClaim();
            var invites = await inviteRepository.GetPendingInvitesForAdmin(userId);
            if (invites is null)
                return Result.Failure<IEnumerable<InviteView>>(new Error("404", "Nenhum convite encontrado"));

            var inviteViews = invites.Select(i => new InviteView
            {
                Id = i.Id,
                GroupDescription = i.Group.Description,
                UserName = i.User.UserName,
                GroupName = i.Group.Name,
                Status = i.Status
            });

            return Result.Success(inviteViews);
        }

        public async Task<Result<IdView>> Accept(Guid id)
        {
            var invite = await inviteRepository.GetById(id);
            if (invite is null)
                return Result.Failure<IdView>(new Error("404", $"Convite de código {id} não encontrado"));

            var validationGroupAdmin = await groupAdminValidator.Validate(invite.GroupId);
            if (!validationGroupAdmin.IsSuccess)
                return Result.Failure<IdView>(new Error(validationGroupAdmin.Error.Code, validationGroupAdmin.Error.Message));

            await inviteService.Accept(invite);

            if (notifier.HasNotification())
                return Result.Failure<IdView>(new Error("400", notifier.GetNotificationMessage()));

            var userGroup = new UserGroup(invite.UserId, invite.GroupId, false, false);

            await userGroupService.Add(userGroup);

            if (notifier.HasNotification())
                return Result.Failure<IdView>(new Error("400", notifier.GetNotificationMessage()));

            await unityOfWork.CommitAsync();

            return Result.Success(new IdView(invite.Id));
        }

        public async Task<Result<IdView>> Reject(Guid id)
        {
            var invite = await inviteRepository.GetById(id);
            if (invite is null)
                return Result.Failure<IdView>(new Error("404", $"Convite de código {id} não encontrado"));

            var validationGroupAdmin = await groupAdminValidator.Validate(invite.GroupId);
            if (!validationGroupAdmin.IsSuccess)
                return Result.Failure<IdView>(new Error(validationGroupAdmin.Error.Code, validationGroupAdmin.Error.Message));

            await inviteService.Reject(invite);
            if (notifier.HasNotification())
                return Result.Failure<IdView>(new Error("400", notifier.GetNotificationMessage()));

            await unityOfWork.CommitAsync();

            return Result.Success(new IdView(invite.Id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
