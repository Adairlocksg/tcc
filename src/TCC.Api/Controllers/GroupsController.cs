using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.Application.Dtos;
using TCC.Application.Services.Groups;

namespace TCC.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class GroupsController(IGroupAppService groupAppService) : MainController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GroupDto dto)
        {
            return await Execute(() => groupAppService.Add(dto));
        }
    }
}
