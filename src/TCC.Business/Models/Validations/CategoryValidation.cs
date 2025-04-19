using FluentValidation;

namespace TCC.Business.Models.Validations
{
    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(g => g.Description)
              .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
              .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
