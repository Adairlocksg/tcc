using FluentValidation;

namespace TCC.Business.Models.Validations
{
    public class ExpenseValidation : AbstractValidator<Expense>
    {
        public ExpenseValidation()
        {
            RuleFor(g => g.Description)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(2, 200).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(g => g.Value)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que zero");

            RuleFor(g => g.BeginDate)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(g => g.EndDate)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .GreaterThan(g => g.BeginDate).WithMessage("O campo {PropertyName} deve ser maior que a data de início");

            RuleFor(g => g.CategoryId)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Must(g => g != Guid.Empty).WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(g => g.UserId)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Must(g => g != Guid.Empty).WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(g => g.GroupId)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Must(g => g != Guid.Empty).WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(g => g.Recurrence)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .IsInEnum().WithMessage("O campo {PropertyName} deve ser um valor válido");

            RuleFor(g => g.RecurrenceInterval)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(g => g.Active)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Must(g => g == true || g == false).WithMessage("O campo {PropertyName} deve ser um valor válido");
        }
    }
}
