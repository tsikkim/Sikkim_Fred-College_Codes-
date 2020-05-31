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

        public bool IsUserExists(string emailId)
        {
            return this.userRepository.IsUserExists(emailId);
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

        public void DeleteUserByEmailId(string emailId)
        {
            var user = this.userRepository.GetUserByEmailId(emailId);

            if(user != null)
            {
                this.userRepository.DeleteUser(user);
            }
        }

        public bool ApproveUser(string emailId)
        {
            var user = this.userRepository.GetUserByEmailId(emailId);

            if (user != null)
            {
                user.IsActive = true;

                this.userRepository.UpdateUser(user);

                var decryptedPassword = this.cryptoService.Decrypt(user.Password);
                LoginDetailsEmailModel emailModel = new LoginDetailsEmailModel();
                emailModel.ReceiverEmail = emailId;
                emailModel.ReceiverName = string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) ? "user" : user.FirstName + " " + user.LastName;
                emailModel.Password = decryptedPassword;
                emailModel.EmailId = emailId;
                emailModel.Subject = "Login details";
                this.emailService.SendLoginDetails(emailModel);
            }

            return true;
        }

        public void SendLoginDetails(string emailId)
        {
            var user = this.userRepository.GetUserByEmailId(emailId);

            if(user == null)
            {
                throw new NotFoundException($"User with emailId {emailId} does not exist.");
            }

            var password = this.cryptoService.Decrypt(user.Password);

            var emailModel = new LoginDetailsEmailModel();

            emailModel.ReceiverEmail = emailId;
            emailModel.ReceiverName = emailId;
            emailModel.Password = password;
            emailModel.EmailId = emailId;
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
