using System.Collections.Generic;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IDDORegistraionService
    {
        DDORegistration SaveRegistration(DDORegistrationModel registration);

        List<DDORegistration> GetPendingRegistrations();

        List<DDORegistration> GetApprovedRegistratins();

        void RejectDDORegistration(long ddoRegistrationId);
    }
}
