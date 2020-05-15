using SikkimGov.Platform.Business.Common.Contracts;
using SikkimGov.Platform.Business.Common.Utilities;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        public ICryptoService cryptoService;

        public UserService(IUserRepository userRepository, ICryptoService cryptoService)
        {
            this.userRepository = userRepository;
            this.cryptoService = cryptoService;
        }

        public bool IsUserExists(string userName)
        {
            return this.userRepository.IsUserExists(userName);
        }

        public User SaveUser(User user)
        {
            if(string.IsNullOrEmpty(user.Password))
            {
                user.Password = cryptoService.Encrypt(PasswordGenerator.GenerateRandomPassword(8));
            }

            return this.userRepository.SaveUser(user);
        }
    }
}
