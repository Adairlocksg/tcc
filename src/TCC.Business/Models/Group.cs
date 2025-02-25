namespace TCC.Business.Models
{
    public class Group : Entity
    {
        public string Description { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
