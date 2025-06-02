namespace TCC.Application.Dtos
{
    public class GetExpenseSummaryByGroupDto
    {
        public Guid GroupId { get; set; }
        public Guid? CategoryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
