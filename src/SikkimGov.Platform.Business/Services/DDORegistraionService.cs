using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
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

        public DDORegistration SaveRegistration(DDORegistration registration)
        {
            return this.repository.SaveDDORegistration(registration);
        }
    }
}
