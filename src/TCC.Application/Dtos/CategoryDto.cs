using System.ComponentModel.DataAnnotations;

namespace TCC.Application.Dtos
{
    public class CategoryDto
    {
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Description { get; set; }
    }
}
