using System.ComponentModel.DataAnnotations;

namespace TCC.Application.Dtos
{
    public class UserDto
    {
        [Display(Name = "Primeiro nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Último nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; } = string.Empty;
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Password { get; set; } = string.Empty;
    }
}
