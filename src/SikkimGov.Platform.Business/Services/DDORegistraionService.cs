using System;
using System.Collections.Generic;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.Common.Utilities;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.Business.Services
{
    public class DDORegistraionService : IDDORegistraionService
    {
        private readonly IDDORegistrationRepository repository;
        private readonly IUserService userService;

        public DDORegistraionService(IDDORegistrationRepository repository, IUserService userService)
        {
            this.repository = repository;
            this.userService = userService;
        }

        public DDORegistration SaveRegistration(DDORegistrationModel registrationModel)
        {
            var userExist = this.userService.IsUserExists(registrationModel.EmailId);

            if (userExist)
            {
                throw new UserAlreadyExistsException($"User with email {registrationModel.EmailId} already exist.");
            }

            var ddoRegistration = new DDORegistration();
            ddoRegistration.DepartmentID = registrationModel.DepartmentId;
            ddoRegistration.DistrictID= registrationModel.DistrictId;
            ddoRegistration.DesignationID = registrationModel.DesignationId;
            ddoRegistration.DDOCode = registrationModel.DDOCode;
            ddoRegistration.EmailID = registrationModel.EmailId;
            ddoRegistration.ContactNumber = registrationModel.ContactNumber;
            ddoRegistration.OfficeAddress1 = registrationModel.OfficeAddress1;
            ddoRegistration.OfficeAddress2 = registrationModel.OfficeAddress2;
            ddoRegistration.TINNumber = registrationModel.TINNumber;
            ddoRegistration.TANNumber = registrationModel.TANNumber;
            ddoRegistration.CreatedDate = DateTime.Now;
            ddoRegistration.IsApproved = false;

            var newRegistration = this.repository.CreateDDORegistration(ddoRegistration);

            var user = new User();
            user.UserType = UserType.DDOUser;
            user.EmailID = registrationModel.EmailId;
            user.IsActive = false;

            this.userService.CreateUser(user, PasswordGenerator.GenerateRandomPassword(8));

            return newRegistration;
        }

        public List<Models.DomainModels.DDORegistrationDetails> GetAllRegistrations()
        {
            return this.repository.GetDDORegistrationsByStatus(null);
        }

        public List<Models.DomainModels.DDORegistrationDetails> GetPendingRegistrations()
        {
            return this.repository.GetDDORegistrationsByStatus(false);
        }
        
        public List<Models.DomainModels.DDORegistrationDetails> GetApprovedRegistrations()
        {
            return this.repository.GetDDORegistrationsByStatus(true);
        }

        public void ApproveDDORegistration(long ddoRegistrationId, int approvedby)
        {
            var registration = this.repository.GetDDORegistrationById(ddoRegistrationId);

            if (registration != null)
            {
                registration.IsApproved = true;
                registration.ApprovedBy = approvedby;
                registration.ApprovedDate = DateTime.Now;

                var emailId = registration.EmailID;

                this.repository.UpdateRegistration(registration);
                this.userService.ApproveUser(emailId);
            }
            else
            {
                throw new NotFoundException($"DDORegistration with ID {ddoRegistrationId} does not exist.");
            }
        }

        public void RejectDDORegistration(long ddoRegistrationId)
        {
            var registration = this.repository.GetDDORegistrationById(ddoRegistrationId);

            if (registration != null)
            {
                var emailId = registration.EmailID;

                this.repository.DeleteDDORegistration(registration);
                this.userService.DeleteUserByUserName(emailId);
            }
            else
            {
                throw new NotFoundException($"DDORegistration with ID {ddoRegistrationId} does not exist.");
            }
        }
    }
}