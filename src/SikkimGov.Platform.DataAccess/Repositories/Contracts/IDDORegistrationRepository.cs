using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IDDORegistrationRepository
    {
        void SaveDDORegistration(DDORegistration ddoRegistration);
    }
}
