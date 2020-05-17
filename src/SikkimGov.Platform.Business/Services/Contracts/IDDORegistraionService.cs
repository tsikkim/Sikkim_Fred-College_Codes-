using System.Collections.Generic;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IDDORegistraionService
    {
        DDORegistration SaveRegistration(DDORegistrationModel registration);

        List<DDORegistrationDetails> GetAllRegistrations();

        List<DDORegistrationDetails> GetPendingRegistrations();

        List<DDORegistrationDetails> GetApprovedRegistrations();

        void RejectDDORegistration(long ddoRegistrationId);

        void ApproveDDORegistration(long ddoRegistrationId, int approvedby);
    }
}
