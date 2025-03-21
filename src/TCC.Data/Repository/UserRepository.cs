using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Data.Context;

namespace TCC.Data.Repository
{
    public class UserRepository(MyDbContext db) : Repository<User>(db), IUserRepository
    {
        public async Task<User> GetByEmail(string email)
        {
            var users = await Where(u => u.Email == email);

            return users?.SingleOrDefault();
        }

        public async Task<User> GetByUsername(string userName)
        {
            var users = await Where(u => u.UserName == userName);

            return users?.SingleOrDefault();
        }
    }
}
