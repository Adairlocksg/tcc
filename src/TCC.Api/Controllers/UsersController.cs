using Microsoft.AspNetCore.Mvc;
using TCC.Application.Services.Users;
using TCC.Application.ViewModels;
using TCC.Business.Interfaces;

namespace TCC.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController(IUserAppService userAppService, INotifier notifier) : MainController(notifier)
    {
        [HttpPost]
        public async Task<IActionResult> Add(UserViewModel userVwm)
        {
            return await Execute(() => userAppService.Add(userVwm));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UserViewModel userVwm)
        {
            return await Execute(() => userAppService.Update(id, userVwm));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserViewModel>> GetById(Guid id)
        {
            return await Execute(() => userAppService.GetById(id));
        }
    }
}
