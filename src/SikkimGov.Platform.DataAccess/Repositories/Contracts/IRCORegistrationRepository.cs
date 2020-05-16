using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IRCORegistrationRepository
    {
        RCORegistration SaveRCORegistration(RCORegistration rcoRegistration);

        bool DeleteRCORegistration(long rcoRegistrationId);

        RCORegistration GetRCORegistrationById(long rcoRegistrationId);
    }
}
