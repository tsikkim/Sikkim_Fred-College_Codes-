using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Security.Contracts;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.ApiModels;

namespace SikkimGov.Platform.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository userRepository;
        private readonly ICryptoService cryptoService;
        private readonly ITokenService tokenService;

        public AuthenticationService(IUserRepository userRepository, ICryptoService cryptoService, ITokenService tokenService)
        {
            this.userRepository = userRepository;
            this.cryptoService = cryptoService;
            this.tokenService = tokenService;
        }

        public AuthenticationResult AuthenticateUser(LoginModel loginModel)
        {
            var result = new AuthenticationResult();

            var user = this.userRepository.GetUserByUserName(loginModel.UserName);

            if (user != null)
            {
                var encryptedPassword = this.cryptoService.Encrypt(loginModel.Password);

                if (encryptedPassword == user.Password)
                {
                    result.IsAuthenticated = true;
                    result.UserName = user.UserName;
                    result.IsAdmin = user.IsAdmin;
                    result.DDOCode = user.DDOCode;
                    result.DepartmentId = user.DepartmentId.HasValue ? user.DepartmentId.Value : 0;
                    result.IsDDO = user.IsDDOUser;
                    result.IsRCO = user.IsRCOUser;
                    result.IsSuperAdmin = user.IsSuperAdmin;
                    result.UserId = user.Id;

                    var token = this.tokenService.GenerateJSONWebToken(result);

                    result.AccessToken = token;

                    return result;
                }
            }

            return result;
        }
    }
}
