using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Models.ApiModels;

namespace SikkimGov.Platform.Business.Services
{
    public class UserService : IUserService
    {
        public AuthenticationResult AuthenticateUser(LoginModel loginModel)
        {
            return new AuthenticationResult();
        }
    }
}
