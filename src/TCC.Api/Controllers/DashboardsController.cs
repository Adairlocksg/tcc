using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.Application.Services.Dashboards;

namespace TCC.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardsController(IDashboardAppService dashboardAppService) : MainController

    {
        [HttpGet("Main")]
        public async Task<IActionResult> GetMainDashboard()
        {
            return await Execute(() => dashboardAppService.GetMainDashboard());
        }
    }
}
