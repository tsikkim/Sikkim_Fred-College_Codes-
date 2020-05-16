using System.Collections.Generic;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IDDORegistraionService
    {
        DDORegistration SaveRegistration(DDORegistrationModel registration);

        List<DDORegistration> GetPendingRegistrations();

        List<DDORegistration> GetApprovedRegistrations();

        void RejectDDORegistration(long ddoRegistrationId);

        void ApproveDDORegistration(long ddoRegistrationId, int approvedby);
    }
}
