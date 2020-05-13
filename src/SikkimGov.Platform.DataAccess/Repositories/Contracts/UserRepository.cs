using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public class UserRepository : IUserRepository
    {
        public User GetUserByUserName(string userName)
        {
            return new User();
        }
    }
}
