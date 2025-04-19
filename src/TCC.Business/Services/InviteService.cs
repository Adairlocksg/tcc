using Microsoft.AspNetCore.Mvc.ViewEngines;
using TCC.Business.Base;
using TCC.Business.Interfaces;
using TCC.Business.Models;

namespace TCC.Business.Services
{
    public class InviteService(INotifier notifier,
                               IInviteRepository inviteRepository,
                               IUserGroupRepository userGroupRepository) : BaseService(notifier), IInviteService
    {
        public async Task Add(Invite invite)
        {
            var userGroup = await userGroupRepository.GetByUserAndGroup(invite.UserId, invite.GroupId);
            if (userGroup is not null)
            {
                Notify("Usuário já foi adicionado a esse grupo");
                return;
            }

            var existentInvite = await inviteRepository.GetByUserAndGroup(invite.UserId, invite.GroupId);
            if (existentInvite is not null && existentInvite.IsPending)
            {
                Notify("Usuário já possui convite pendente");
                return;
            }

            await inviteRepository.Add(invite);
        }

        public async Task Accept(Invite invite)
        {
            if (!invite.IsPending)
            {
                Notify("Convite já foi aceito ou rejeitado");
                return;
            }

            invite.Accept();

            await inviteRepository.Update(invite);
        }

        public async Task Reject(Invite invite)
        {
            if (!invite.IsPending)
            {
                Notify("Convite já foi aceito ou rejeitado");
                return;
            }

            invite.Reject();

            await inviteRepository.Update(invite);
        }
    }
}
