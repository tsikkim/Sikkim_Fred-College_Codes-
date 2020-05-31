using System.Collections.Generic;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IUserRepository
    {
        bool IsUserExists(string emailId);

        User SaveUser(User user);

        User UpdateUser(User user);

        bool DeleteUser(User user);

        User GetUserByEmailId(string emailId);

        List<Models.DomainModels.UserDetails> GetDDOUserDetails();

        List<Models.DomainModels.UserDetails> GetRCOUserDetails();

        List<Models.DomainModels.UserDetails> GetAdminUserDetails();
    }
}
