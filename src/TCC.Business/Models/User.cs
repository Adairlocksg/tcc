namespace TCC.Business.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        //EF Relation
        public IEnumerable<UserGroup> UserGroups { get; set; }
    }
}
