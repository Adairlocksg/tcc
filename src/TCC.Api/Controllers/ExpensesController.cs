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

        [HttpGet("SuumaryByGroup")]
        public Task<IActionResult> GetExpenseSummaryByGroup([FromQuery] GetExpenseSummaryByGroupDto dto)
        {
            return  Execute(() => expenseAppService.GetExpenseSummaryByGroup(dto));
        }
    }
}
