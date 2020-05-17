using System.Collections.Generic;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IUserRepository
    {
        bool IsUserExists(string userName);

        User SaveUser(User user);

        void DeleteUser(long userId);

        void DeleteUserByUserName(string emailId);

        bool UpdateUserStatusByUserName(string userName, bool status);

        User GetUserByUsername(string userName);

        List<UserDetails> GetDDOUserDetails();

        List<UserDetails> GetRCOUserDetails();

        List<UserDetails> GetAdminUserDetails();
    }
}
