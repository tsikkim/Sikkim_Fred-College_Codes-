using System.Collections.Generic;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IUserService
    {
        bool IsUserExists(string userName);

        User CreateUser(User user, string password);

        void DeleteUser(long userId);

        void DeleteUserByUserName(string emailId);

        bool ApproveUser(string userName);

        void SendLoginDetails(string userName);

        List<Models.DomainModels.UserDetails> GetDDOUserDetails();

        List<Models.DomainModels.UserDetails> GetRCOUserDetails();

        List<Models.DomainModels.UserDetails> GetAdminUserDetails();
    }
}
