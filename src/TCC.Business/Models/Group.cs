namespace TCC.Business.Models
{
    public class Group : Entity
    {
        public Group() { }
        public Group(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }
        public bool Active { get; private set; } = true;

        //EF Relation
        public IEnumerable<UserGroup> UserGroups { get; set; }
    }
}
