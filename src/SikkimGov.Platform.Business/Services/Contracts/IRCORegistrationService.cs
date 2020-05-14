using SikkimGov.Platform.Models.ApiModels;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IRCORegistrationService
    {
        RCORegistrationModel SaveRegistration(RCORegistrationModel registrationModel);
    }
}
