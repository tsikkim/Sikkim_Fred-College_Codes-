using SikkimGov.Platform.Models.ApiModels;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IAuthenticationService
    {
        AuthenticationResult AuthenticateUser(LoginModel loginModel);
    }
}
