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

        [HttpPut("{id:guid}/Favorite")]
        public async Task<IActionResult> Favorite(Guid id)
        {
            return await Execute(() => groupAppService.Favorite(id));
        }

        [HttpPut("{id:guid}/Unfavorite")]
        public async Task<IActionResult> Unfavorite(Guid id)
        {
            return await Execute(() => groupAppService.Unfavorite(id));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GroupDto dto)
        {
            return await Execute(() => groupAppService.Update(id, dto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await Execute(() => groupAppService.GetAll());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await Execute(() => groupAppService.GetById(id));
        }

        [HttpGet("{id:guid}/Link")]
        public async Task<IActionResult> GenerateLink(Guid id)
        {
            return await Execute(() => groupAppService.GenerateLink(id));
        }

        [HttpPost("{id:guid}/Categories")]
        public async Task<IActionResult> AddCategory(Guid id, [FromBody] CategoryDto dto)
        {
            return await Execute(() => groupAppService.AddCategory(id, dto));
        }

        [HttpPut("{id:guid}/Categories/{categoryId:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, Guid categoryId, [FromBody] CategoryDto dto)
        {
            return await Execute(() => groupAppService.UpdateCategory(id, categoryId, dto));
        }

        [HttpGet("{id:guid}/Categories")]
        public async Task<IActionResult> GetCategories(Guid id)
        {
            return await Execute(() => groupAppService.GetCategories(id));
        }

        [HttpGet("{id:guid}/Members")]
        public async Task<IActionResult> GetMembers(Guid id)
        {
            return await Execute(() => groupAppService.GetMembers(id));
        }
    }
}
