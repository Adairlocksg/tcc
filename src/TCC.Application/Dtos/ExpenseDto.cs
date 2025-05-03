using System.ComponentModel.DataAnnotations;
using TCC.Business.Models;

namespace TCC.Application.Dtos
{
    public class ExpenseDto
    {
        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O campo {0} deve ser maior que zero")]
        [Display(Name = "Valor")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [Display(Name = "Data de Início")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Data de Fim")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [Display(Name = "É Recorrente")]
        public bool IsRecurring { get; set; } = false;

        [Display(Name = "Tipo de Recorrência")]
        public RecurrenceType? Recurrence { get; set; }

        [Display(Name = "Intervalo de Recorrência")]
        public int RecurrenceInterval { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [Display(Name = "Ativo")]
        public bool Active { get; set; } = true;
        public Guid? UserId { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [Display(Name = "Categoria")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [Display(Name = "Grupo")]
        public Guid GroupId { get; set; }
    }
}
