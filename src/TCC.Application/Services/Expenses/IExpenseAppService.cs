using TCC.Application.Dtos;
using TCC.Application.Views;
using TCC.Business.Base;

namespace TCC.Application.Services.Expenses
{
    public interface IExpenseAppService : IDisposable
    {
        Task<Result<IdView>> Add(ExpenseDto dto);
    }
}
