using System.Collections.Generic;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.DomainModels;

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
            ddoRegistration.DepartmentId = registrationModel.DepartmentId;
            ddoRegistration.DistrictId = registrationModel.DistrictId;
            ddoRegistration.DesignationId = registrationModel.DesignationId;
            ddoRegistration.DDOCode = registrationModel.DDOCode;
            ddoRegistration.EmailId = registrationModel.EmailId;
            ddoRegistration.ContactNumber = registrationModel.ContactNumber;
            ddoRegistration.OfficeAddress1 = registrationModel.OfficeAddress1;
            ddoRegistration.OfficeAddress2 = registrationModel.OfficeAddress2;
            ddoRegistration.TINNumber = registrationModel.TINNumber;
            ddoRegistration.TANNumber = registrationModel.TANNumber;

            var newRegistration = this.repository.CreateDDORegistration(ddoRegistration);

            var user = new User();
            user.DDOCode = registrationModel.DDOCode;
            user.IsDDOUser = true;
            user.UserName = registrationModel.EmailId;
            user.EmailId = registrationModel.EmailId;
            user.DepartmentId = registrationModel.DepartmentId;

            this.userService.CreateUser(user);

            return newRegistration;
        }

        public List<DDORegistrationDetails> GetAllRegistrations()
        {
            return this.repository.GetDDORegistrationsByStatus(null);
        }

        public List<DDORegistrationDetails> GetPendingRegistrations()
        {
            return this.repository.GetDDORegistrationsByStatus(false);
        }

        public List<DDORegistrationDetails> GetApprovedRegistrations()
        {
            return this.repository.GetDDORegistrationsByStatus(true);
        }

        public void ApproveDDORegistration(long ddoRegistrationId, int approvedby)
        {
            var registration = this.repository.GetDDORegistrationById(ddoRegistrationId);

            if (registration != null)
            {
                var emailId = registration.EmailId;

                this.repository.UpdateDDORegistrationStatus(ddoRegistrationId, true, approvedby);
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
                var emailId = registration.EmailId;

                this.repository.DeleteDDORegistration(ddoRegistrationId);
                this.userService.DeleteUserByUserName(emailId);
            }
            else
            {
                throw new NotFoundException($"DDORegistration with ID {ddoRegistrationId} does not exist.");
            }
        }
    }
}
