namespace TCC.Application.Views
{
    public class CategoryView : IdView
    {
        public string Description { get; private set; }
        public Guid GroupId { get; private set; }
        public bool Active { get; private set; } = true;
    }
}
