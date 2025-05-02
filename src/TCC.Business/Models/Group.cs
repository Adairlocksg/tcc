namespace TCC.Business.Models
{
    public class Group : Entity
    {
        public Group() { }
        public Group(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; } = true;
        public ICollection<Category> Categories { get; private set; } = [];

        //EF Relation
        public IEnumerable<UserGroup> UserGroups { get; set; }
    }
}
