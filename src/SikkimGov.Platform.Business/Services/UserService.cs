using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Security.Contracts;
using SikkimGov.Platform.Common.Utilities;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private ICryptoService cryptoService;

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

        public void DeleteUser(long userId)
        {
            this.userRepository.DeleteUser(userId);
        }

        public void DeleteUserByEmailId(string emailId)
        {
            this.userRepository.DeleteUserByEmailId(emailId);
        }
    }
}
