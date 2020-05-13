using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IUserRepository
    {
        User GetUserByUserName(string userName);
    }
}
