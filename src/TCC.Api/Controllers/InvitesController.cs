using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.Application.Dtos;
using TCC.Application.Services.Groups;
using TCC.Application.Services.Invites;

namespace TCC.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class InvitesController(IInviteAppService inviteAppService) : MainController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InviteDto dto)
        {
            return await Execute(() => inviteAppService.Add(dto));
        }

        [HttpGet("Pending")]
        public async Task<IActionResult> GetPendingInvitesForAdmin()
        {
            return await Execute(() => inviteAppService.GetPendingInvitesForAdmin());
        }

        [HttpPut("{id:guid}/Accept")]
        public async Task<IActionResult> Accept([FromRoute] Guid id)
        {
            return await Execute(() => inviteAppService.Accept(id));
        }

        [HttpPut("{id:guid}/Reject")]
        public async Task<IActionResult> Reject([FromRoute] Guid id)
        {
            return await Execute(() => inviteAppService.Reject(id));
        }
    }
}
