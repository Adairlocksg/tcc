using Microsoft.AspNetCore.Mvc;
using TCC.Application.Dtos;
using TCC.Application.Services.Users;

namespace TCC.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController(IUserAppService userAppService) : MainController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto dto)
        {
            return await Execute(() => userAppService.Register(dto));
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
