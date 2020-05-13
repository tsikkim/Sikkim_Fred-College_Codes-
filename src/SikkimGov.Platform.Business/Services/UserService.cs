using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.ApiModels;

namespace SikkimGov.Platform.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public AuthenticationResult AuthenticateUser(LoginModel loginModel)
        {
            return new AuthenticationResult();
        }

        public bool IsUserExists(string userName)
        {
            return this.userRepository.IsUserExists(userName);
        }
    }
}
