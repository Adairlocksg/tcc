using TCC.Application.Views;
using TCC.Business.Base;

namespace TCC.Application.Services.Dashboards
{
    public interface IDashboardAppService
    {
        Task<Result<MainDashboardView>> GetMainDashboard();
    }
}
