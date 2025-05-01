using FluentValidation;

namespace TCC.Business.Models.Validations
{
    public class GroupValidation : AbstractValidator<Group>
    {
        public GroupValidation()
        {
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(g => g.Description)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(g => g.Active)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");
        }
    }
}
