using SikkimGov.Platform.Common.Models;

namespace SikkimGov.Platform.Common.External.Contracts
{
    public interface IEmailService
    {
        void SendLoginDetails(LoginDetailsEmailModel model);
    }
}
