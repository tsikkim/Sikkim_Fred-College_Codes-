using System.Collections.Generic;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IUserRepository
    {
        bool IsUserExists(string userName);

        User SaveUser(User user);

        User UpdateUser(User user);

        bool DeleteUser(User user);

        void DeleteUser(long userId);

        User GetUserByUsername(string userName);

        List<Models.DomainModels.UserDetails> GetDDOUserDetails();

        List<Models.DomainModels.UserDetails> GetRCOUserDetails();

        List<Models.DomainModels.UserDetails> GetAdminUserDetails();
    }
}
