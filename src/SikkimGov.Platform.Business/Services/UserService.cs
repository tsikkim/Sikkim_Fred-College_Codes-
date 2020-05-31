using System;
using System.Collections.Generic;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.Common.External.Contracts;
using SikkimGov.Platform.Common.Models;
using SikkimGov.Platform.Common.Security.Contracts;
using SikkimGov.Platform.Common.Utilities;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.Domain;

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

        public User CreateUser(User user, string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password", "Password can not null or empty");
            }

            user.CreatedDate = DateTime.Now;
            user.Password = cryptoService.Encrypt(password);

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
                user.IsActive = true;
                this.userRepository.UpdateUser(user);

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

        public List<Models.DomainModels.UserDetails> GetDDOUserDetails()
        {
            return this.userRepository.GetDDOUserDetails();
        }

        public List<Models.DomainModels.UserDetails> GetRCOUserDetails()
        {
            return this.userRepository.GetRCOUserDetails();
        }

        public List<Models.DomainModels.UserDetails> GetAdminUserDetails()
        {
            return this.userRepository.GetAdminUserDetails();
        }
    }
}
