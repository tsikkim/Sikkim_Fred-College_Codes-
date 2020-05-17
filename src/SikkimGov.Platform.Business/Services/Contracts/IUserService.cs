using System.Collections.Generic;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IUserService
    {
        bool IsUserExists(string userName);

        User CreateUser(User user);

        void DeleteUser(long userId);

        void DeleteUserByUserName(string emailId);

        bool ApproveUser(string userName);

        void SendLoginDetails(string userName);

        List<UserDetails> GetDDOUserDetails();
    }
}
