using TCC.Business.Models;

namespace TCC.Application.Views
{
    public class ExpenseView : IdView
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsRecurring { get; set; } = false;
        public RecurrenceType? Recurrence { get; set; }
        public int RecurrenceInterval { get; set; }
        public bool Active { get; set; } = true;
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid GroupId { get; set; }
    }
}
