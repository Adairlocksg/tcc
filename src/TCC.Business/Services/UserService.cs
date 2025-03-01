using TCC.Business.Interfaces;
using TCC.Business.Models;

namespace TCC.Business.Services
{
    public class UserService(IUserRepository userRepository) : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task Add(User user)
        {
            await _userRepository.Add(user);
        }

        public async Task Update(User user)
        {
            await _userRepository.Update(user);
        }

        public async Task Remove(Guid id)
        {
            await _userRepository.Remove(id);
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
