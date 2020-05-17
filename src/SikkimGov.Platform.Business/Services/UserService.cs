using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.Common.External.Contracts;
using SikkimGov.Platform.Common.Models;
using SikkimGov.Platform.Common.Security.Contracts;
using SikkimGov.Platform.Common.Utilities;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ICryptoService cryptoService;
        private readonly IEmailService emailService;

        public UserService(IUserRepository userRepository, ICryptoService cryptoService, IEmailService emailService)
        {
            this.userRepository = userRepository;
            this.cryptoService = cryptoService;
            this.emailService = emailService;
        }

        public bool IsUserExists(string userName)
        {
            return this.userRepository.IsUserExists(userName);
        }

        public User CreateUser(User user)
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

        public void DeleteUserByUserName(string userName)
        {
            this.userRepository.DeleteUserByUserName(userName);
        }

        public bool ApproveUser(string userName)
        {
            var result =  this.userRepository.UpdateUserStatusByUserName(userName, true);

            var user = this.userRepository.GetUserByUsername(userName);

            if (user != null)
            {
                var decryptedPassword = this.cryptoService.Decrypt(user.Password);
                LoginDetailsEmailModel emailModel = new LoginDetailsEmailModel();
                emailModel.ReceiverEmail = userName;
                emailModel.UserName = userName;
                emailModel.Password = decryptedPassword;
                emailModel.EmailId = userName;
                emailModel.Subject = "Login details";
                this.emailService.SendLoginDetails(emailModel);
            }

            return result;
        }

        public void SendLoginDetails(string userName)
        {
            var user = this.userRepository.GetUserByUsername(userName);

            if(user == null)
            {
                throw new NotFoundException($"User with username {userName} does not exist.");
            }

            var password = this.cryptoService.Decrypt(user.Password);

            var emailModel = new LoginDetailsEmailModel();

            emailModel.ReceiverEmail = userName;
            emailModel.UserName = userName;
            emailModel.Password = password;
            emailModel.EmailId = userName;
            emailModel.Subject = "Login details";
            this.emailService.SendLoginDetails(emailModel);
        }
    }
}
