namespace TCC.Application.Views
{
    public class ExpenseSummaryView: IdView
    {
        public string Description { get; set; }
        public decimal TotalValue { get; set; }
        public string RecurrenceType { get; set; }
        public bool IsRecurring { get; set; }
    }
}
