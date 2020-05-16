using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services
{
    public class RCORegistrationService : IRCORegistrationService
    {
        private readonly IRCORegistrationRepository repository;
        private readonly IUserService userService;

        public RCORegistrationService(IRCORegistrationRepository repository, IUserService userService)
        {
            this.repository = repository;
            this.userService = userService;
        }

        public RCORegistrationModel SaveRegistration(RCORegistrationModel registrationModel)
        {
            var userExist = this.userService.IsUserExists(registrationModel.EmailId);

            if(userExist)
            {
                throw new UserAlreadyExistsException($"User with email {registrationModel.EmailId} already exist.");
            }

            var registration = new RCORegistration();

            registration.AdminName = registrationModel.AdminName;
            registration.ContactNumber = registrationModel.ContactNumber;
            registration.Department = registrationModel.Department;
            registration.Designation = registrationModel.Designation;
            registration.District = registrationModel.District;
            registration.EmailId = registrationModel.EmailId;
            registration.OfficeAddress1 = registrationModel.OfficeAddress1;
            registration.OfficeAddress2 = registrationModel.OfficeAddress2;
            registration.RegistrationType = registrationModel.RegistrationType;
            registration.TINNumber = registrationModel.TINNumber;
            registration.TANNumber = registrationModel.TANNumber;

            this.repository.SaveRCORegistration(registration);

            var user = new User();
            user.EmailId = registrationModel.EmailId;
            user.IsRCOUser = true;
            user.UserName = registrationModel.EmailId;
            user.EmailId = registrationModel.EmailId;

            this.userService.SaveUser(user);

            return registrationModel;
        }

        public void RejectRCORegistration(long rcoRegistrationId)
        {
            var registration = this.repository.GetRCORegistrationById(rcoRegistrationId);

            if (registration != null)
            {
                var emailId = registration.EmailId;

                this.repository.DeleteRCORegistration(rcoRegistrationId);
                this.userService.DeleteUserByEmailId(emailId);
            }
            else
            {
                throw new NotFoundException($"RCORegistration with {rcoRegistrationId} does not exist.");
            }
        }
    }
}
