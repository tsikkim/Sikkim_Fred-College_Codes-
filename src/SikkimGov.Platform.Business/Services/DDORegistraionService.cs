using System.Collections.Generic;
using SikkimGov.Platform.Business.Services.Contracts;
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

            var newRegistration = this.repository.SaveDDORegistration(ddoRegistration);

            var user = new User();
            user.DDOCode = registration.DDOCode;
            user.IsDDOUser = true;
            user.UserName = registration.EmailId;
            user.EmailId = registration.EmailId;
            user.DepartmentId = registration.DepartmentId;

            this.userService.SaveUser(user);

            return newRegistration;

        }

        public List<DDORegistration> GetPendingRegistrations()
        {
            return new List<DDORegistration>();
        }

        public List<DDORegistration> GetApprovedRegistratins()
        {
            return new List<DDORegistration>();
        }
    }
}
