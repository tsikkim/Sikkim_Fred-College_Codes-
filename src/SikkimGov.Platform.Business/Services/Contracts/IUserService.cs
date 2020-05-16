using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IUserService
    {
        bool IsUserExists(string userName);

        User SaveUser(User user);

        void DeleteUser(long userId);

        void DeleteUserByEmailId(string emailId);
    }
}
