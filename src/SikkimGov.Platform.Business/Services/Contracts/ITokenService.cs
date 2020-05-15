using SikkimGov.Platform.Models.ApiModels;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateJSONWebToken(AuthenticationResult loginResult);
    }
}
