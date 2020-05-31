using System;
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

            var user = this.userRepository.GetUserByUsername(loginModel.UserName);

            if (user != null)
            {
                var encryptedPassword = this.cryptoService.Encrypt(loginModel.Password);

                if (encryptedPassword == user.Password)
                {
                    user.LastLoginDate = DateTime.Now;
                    this.userRepository.UpdateUser(user);
                    result.IsAuthenticated = true;
                    result.IsAdmin = user.UserType == Models.Domain.UserType.Admin;
                    result.IsDDO = user.UserType == Models.Domain.UserType.DDOUser;
                    result.IsRCO = user.UserType == Models.Domain.UserType.RCOUser;
                    result.IsSuperAdmin = user.UserType == Models.Domain.UserType.SuperAdmin;
                    result.UserId = user.UserID;

                    var token = this.tokenService.GenerateJSONWebToken(result);

                    result.AccessToken = token;

                    return result;
                }
            }

            return result;
        }
    }
}
