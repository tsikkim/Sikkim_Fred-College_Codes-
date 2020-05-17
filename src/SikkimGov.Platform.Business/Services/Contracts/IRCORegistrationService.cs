using System.Collections.Generic;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IRCORegistrationService
    {
        RCORegistrationModel SaveRegistration(RCORegistrationModel registrationModel);

        void RejectRCORegistration(long rcoRegistrationId);

        void ApproveRCORegistration(long ddoRegistrationId, int approvedby);

        List<RCORegistrationDetails> GetAllRegistrations();

        List<RCORegistrationDetails> GetPendingRegistrations();

        List<RCORegistrationDetails> GetApprovedRegistrations();        
    }
}
