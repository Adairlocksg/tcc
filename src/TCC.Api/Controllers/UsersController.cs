using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.Application.Dtos;
using TCC.Application.Services.Users;

namespace TCC.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController(IUserAppService userAppService) : MainController
    {
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDto dto)
        {
            return await Execute(() => userAppService.Register(dto));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            return await Execute(() => userAppService.Login(dto));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UserDto dto)
        {
            return await Execute(() => userAppService.Update(id, dto));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await Execute(() => userAppService.GetById(id));
        }
    }
}
