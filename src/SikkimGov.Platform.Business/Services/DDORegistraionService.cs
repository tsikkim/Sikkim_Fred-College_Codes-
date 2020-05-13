using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services
{
    public class DDORegistraionService : IDDORegistraionService
    {
        private readonly IDDORegistrationRepository repository;

        public DDORegistraionService(IDDORegistrationRepository repository)
        {
            this.repository = repository;
        }

        public DDORegistration SaveRegistration(DDORegistrationModel registration)
        {
            var ddoRegistration = new DDORegistration();
            ddoRegistration.DepartmentId = registration.DepartmentId;
            ddoRegistration.DistrictId = registration.DistrictId;
            ddoRegistration.DesignationId = registration.DesignationId;
            ddoRegistration.DDOCode = registration.DDOCode;
            ddoRegistration.EmailId = registration.EmailId;
            ddoRegistration.ContactNumber = registration.ContactNumber;
            ddoRegistration.OfficeAddress1 = registration.OfficeAddress1;
            ddoRegistration.OfficeAddress2 = registration.OfficeAddress2;
            ddoRegistration.TINNumber = registration.TINNumber;
            ddoRegistration.TANNumber = registration.TANNumber;

            return  this.repository.SaveDDORegistration(ddoRegistration);
        }
    }
}
