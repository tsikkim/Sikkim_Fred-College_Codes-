using System;
using System.Collections.Generic;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.Business.Services
{
    public class RCORegistrationService : IRCORegistrationService
    {
        private readonly IRCORegistrationRepository repository;
        private readonly IUserService userService;
        private readonly IDepartmentRepository departmentRepository;
        public RCORegistrationService(IRCORegistrationRepository repository, IUserService userService, IDepartmentRepository departmentRepository)
        {
            this.repository = repository;
            this.userService = userService;
            this.departmentRepository = departmentRepository;
        }

        public RCORegistrationModel SaveRegistration(RCORegistrationModel registrationModel)
        {
            var userExist = this.userService.IsUserExists(registrationModel.EmailId);

            if(userExist)
            {
                throw new UserAlreadyExistsException($"User with email {registrationModel.EmailId} already exist.");
            }

            var department = this.departmentRepository.GetDepartmentById(registrationModel.DepartmentId);

            if(department == null)
            {
                throw new InvalidInputException("DepartmentId is not valid.");
            }

            var registration = new RCORegistration();

            registration.AdminName = registrationModel.AdminName;
            registration.ContactNumber = registrationModel.ContactNumber;
            registration.DepartmentID = registrationModel.DepartmentId;
            registration.Designation = registrationModel.Designation;
            registration.District = registrationModel.District;
            registration.EmailID = registrationModel.EmailId;
            registration.OfficeAddress1 = registrationModel.OfficeAddress1;
            registration.OfficeAddress2 = registrationModel.OfficeAddress2;
            registration.RegistrationType = registrationModel.RegistrationType;
            registration.TINNumber = registrationModel.TINNumber;
            registration.TANNumber = registrationModel.TANNumber;

            this.repository.CreateRCORegistration(registration);

            //var user = new User();
            //user.EmailId = registrationModel.EmailId;
            //user.IsRCOUser = true;
            //user.UserName = registrationModel.EmailId;
            //user.EmailId = registrationModel.EmailId;

            //this.userService.CreateUser(user);

            return registrationModel;
        }

        public void ApproveRCORegistration(long rcoRegistrationId, int approvedby)
        {
            var registration = this.repository.GetRCORegistrationById(rcoRegistrationId);

            if (registration != null)
            {
                var emailId = registration.EmailID;

                registration.IsApproved = true;
                registration.ApprovedDate = DateTime.Now;
                registration.ApprovedBy = approvedby;
                this.repository.UpdateRegistration(registration);
                this.userService.ApproveUser(emailId);
            }
            else
            {
                throw new NotFoundException($"RDORegistration with ID {rcoRegistrationId} does not exist.");
            }
        }

        public void RejectRCORegistration(long rcoRegistrationId)
        {
            var registration = this.repository.GetRCORegistrationById(rcoRegistrationId);

            if (registration != null)
            {
                var emailId = registration.EmailID;

                this.repository.DeleteRCORegistration(registration);
                this.userService.DeleteUserByUserName(emailId);
            }
            else
            {
                throw new NotFoundException($"RCORegistration with ID {rcoRegistrationId} does not exist.");
            }
        }

        public List<Models.DomainModels.RCORegistrationDetails> GetAllRegistrations()
        {
            return this.repository.GetRCORegistrationsByStatus(null);
        }

        public List<Models.DomainModels.RCORegistrationDetails> GetPendingRegistrations()
        {
            return this.repository.GetRCORegistrationsByStatus(false);
        }

        public List<Models.DomainModels.RCORegistrationDetails> GetApprovedRegistrations()
        {
            return this.repository.GetRCORegistrationsByStatus(true);
        }
    }
}
