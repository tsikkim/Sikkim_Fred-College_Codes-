using SikkimGov.Platform.Models.ApiModels;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IUserService
    {
        AuthenticationResult AuthenticateUser(LoginModel loginModel);
    }
}
