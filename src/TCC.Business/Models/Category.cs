namespace TCC.Business.Models
{
    public class Category : Entity
    {
        public Category() { }
        public Category(string description, Guid groupId)
        {
            Description = description;
            GroupId = groupId;
        }
        public string Description { get; private set; }
        public Guid GroupId { get; private set; }
        public bool Active { get; private set; } = true;

        // EF Relation
        public virtual Group Group { get; set; }
    }
}
