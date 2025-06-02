namespace TCC.Application.Views
{
    public class GroupDashboardView
    {
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MembersCount { get; set; }
        public decimal TotalCurrentMonth { get; set; }
        public decimal TotalPreviousMonth { get; set; }
        public decimal PercentageChange { get; set; }
        public bool Favorite { get; set; }
        public bool Admin { get; set; }
    }

    public class MainDashboardView
    {
        public decimal TotalCurrentMonth { get; set; }
        public decimal TotalPreviousMonth { get; set; }
        public decimal PercentageChange { get; set; }
        public List<GroupDashboardView> Groups { get; set; }
    }
}
