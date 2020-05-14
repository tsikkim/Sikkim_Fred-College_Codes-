using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services
{
    public class RCORegistrationService : IRCORegistrationService
    {
        private readonly IRCORegistrationRepository repository;

        public RCORegistrationService(IRCORegistrationRepository repository)
        {
            this.repository = repository;
        }

        public RCORegistrationModel SaveRegistration(RCORegistrationModel registrationModel)
        {
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

            return registrationModel;

        }
    }
}
