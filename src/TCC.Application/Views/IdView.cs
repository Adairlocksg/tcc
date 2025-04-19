namespace TCC.Application.Views
{
    public class IdView
    {
        public IdView() { }
        public IdView(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
