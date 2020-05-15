using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IUserService
    {
        bool IsUserExists(string userName);

        User SaveUser(User user);
    }
}
