using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Business.Models.Validations;

namespace TCC.Business.Services
{
    public class ExpenseService(INotifier notifier, IExpenseRepository expenseRepository) : BaseService(notifier), IExpenseService
    {
        public async Task Add(Expense expense)
        {
            if (!ExecuteValidation(new ExpenseValidation(), expense))
                return;

            await expenseRepository.Add(expense);
        }

        public async Task Remove(Expense expense)
        {
            await expenseRepository.Remove(expense.Id);
        }
    }
}
