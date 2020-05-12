using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IDDORegistraionService
    {
        DDORegistration SaveRegistration(DDORegistration registration);
    }
}
