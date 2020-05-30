using System.Collections.Generic;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IRCORegistrationRepository
    {
        RCORegistration CreateRCORegistration(RCORegistration rcoRegistration);

        bool DeleteRCORegistration(RCORegistration registration);

        RCORegistration GetRCORegistrationById(long rcoRegistrationId);

        RCORegistration UpdateRegistration(RCORegistration rcoRegistration);

        List<Models.DomainModels.RCORegistrationDetails> GetRCORegistrationsByStatus(bool? status);
    }
}
