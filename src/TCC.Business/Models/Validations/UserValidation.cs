using FluentValidation;

namespace TCC.Business.Models.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchdo")
                .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchdo")
                .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchdo")
                .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres")
                .EmailAddress().WithMessage("O campo {PropertyName} está em um formato inválido");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchdo")
                .Length(6, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
