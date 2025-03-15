namespace TCC.Business.Models
{
    public class UserGroup : Entity
    {
        public bool Admin { get; set; }
        public bool Favorite { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }

        //EF Relation
        public User User { get; set; }
        public Group Group { get; set; }
    }
}
