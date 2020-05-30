using System.Collections.Generic;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IDDORegistraionService
    {
        DDORegistration SaveRegistration(DDORegistrationModel registration);

        List<Models.DomainModels.DDORegistrationDetails> GetAllRegistrations();

        List<Models.DomainModels.DDORegistrationDetails> GetPendingRegistrations();

        List<Models.DomainModels.DDORegistrationDetails> GetApprovedRegistrations();

        void RejectDDORegistration(long ddoRegistrationId);

        void ApproveDDORegistration(long ddoRegistrationId, int approvedby);
    }
}
