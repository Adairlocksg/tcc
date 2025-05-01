namespace TCC.Application.Views
{
    public class GroupView : IdView
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public bool Favorite { get; set; }
        public bool Admin { get; set; }
        public int Members { get; set; }
    }
}
