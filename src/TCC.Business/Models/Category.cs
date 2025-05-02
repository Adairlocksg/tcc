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

        public void Update(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }
        public Guid GroupId { get; private set; }
        public bool Active { get; private set; } = true;

        // EF Relation
        public virtual Group Group { get; set; }
    }
}
