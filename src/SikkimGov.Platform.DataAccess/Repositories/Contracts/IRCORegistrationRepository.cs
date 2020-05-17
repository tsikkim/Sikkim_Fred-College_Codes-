using System.Collections.Generic;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IRCORegistrationRepository
    {
        RCORegistration SaveRCORegistration(RCORegistration rcoRegistration);

        bool DeleteRCORegistration(long rcoRegistrationId);

        RCORegistration GetRCORegistrationById(long rcoRegistrationId);

        bool UpdateDDORegistrationStatus(long ddoRegistrationId, bool status, int updatedBy);

        List<RCORegistrationDetails> GetRCORegistrationsByStatus(bool? status);
    }
}
