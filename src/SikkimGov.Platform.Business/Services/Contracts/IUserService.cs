using System.Collections.Generic;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IUserService
    {
        bool IsUserExists(string emailId);

        User CreateUser(User user, string password);

        void DeleteUserByEmailId(string emailId);

        void DeleteUserById(int userId);

        bool ApproveUser(string emailId);

        void SendLoginDetails(string emailId);

        List<Models.DomainModels.UserDetails> GetDDOUserDetails();

        List<Models.DomainModels.UserDetails> GetRCOUserDetails();

        List<Models.DomainModels.UserDetails> GetAdminUserDetails();
    }
}
