using TCC.Infra.Services;

namespace TCC.Business.Models
{
    public class User : Entity
    {
        public User() { }

        public User(string firstName, string lastName, string email, string userName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            Password = password;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

        //EF Relation
        public IEnumerable<UserGroup> UserGroups { get; set; }

        public void Update(string firstName, string lastName, string email, string userName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            Password = password;
        }

        public void EncryptPassword()
        {
            Password = HashService.Encrypt(Password);
        }
    }
}
