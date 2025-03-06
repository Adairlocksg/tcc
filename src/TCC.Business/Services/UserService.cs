using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Business.Models.Validations;

namespace TCC.Business.Services
{
    public class UserService(IUserRepository userRepository, INotifier notifier) : BaseService(notifier), IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task Add(User user)
        {
            if (!ExecuteValidation(new UserValidation(), user))
                return;

            var userWithSameEmail = await _userRepository.Where(u => u.Email == user.Email);

            if (userWithSameEmail.Any())
            {
                Notify("Já existe um usuário com este e-mail informado.");
                return;
            }

            var userWithSameUserName = await _userRepository.Where(u => u.UserName == user.UserName);

            if (userWithSameUserName.Any())
            {
                Notify("Já existe um usuário com este nome de usuário informado.");
                return;
            }

            await _userRepository.Add(user);
        }

        public async Task Update(User user)
        {
            if (await _userRepository.GetById(user.Id) is null)
            {
                Notify($"Usuário de id {user.Id} não encontrado");
                return;
            }

            if (!ExecuteValidation(new UserValidation(), user))
                return;

            var userWithSameEmail = await _userRepository.Where(u => u.Email == user.Email && u.Id != user.Id);

            if (userWithSameEmail.Any())
            {
                Notify("Já existe um usuário com este e-mail informado.");
                return;
            }

            var userWithSameUserName = await _userRepository.Where(u => u.UserName == user.UserName && u.Id != user.Id);

            if (userWithSameUserName.Any())
            {
                Notify("Já existe um usuário com este nome de usuário informado.");
                return;
            }

            await _userRepository.Update(user);
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
