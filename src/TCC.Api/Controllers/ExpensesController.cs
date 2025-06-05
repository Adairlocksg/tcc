using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.Application.Dtos;
using TCC.Application.Services.Expenses;

namespace TCC.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ExpensesController(IExpenseAppService expenseAppService) : MainController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ExpenseDto dto)
        {
            return await Execute(() => expenseAppService.Add(dto));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ExpenseDto dto)
        {
            return await Execute(() => expenseAppService.Update(id, dto));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await Execute(() => expenseAppService.Remove(id));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await Execute(() => expenseAppService.GetById(id));
        }

        [HttpGet("SummaryByGroup")]
        public Task<IActionResult> GetExpenseSummaryByGroup([FromQuery] GetExpenseSummaryByGroupDto dto)
        {
            return  Execute(() => expenseAppService.GetExpenseSummaryByGroup(dto));
        }
    }
}
