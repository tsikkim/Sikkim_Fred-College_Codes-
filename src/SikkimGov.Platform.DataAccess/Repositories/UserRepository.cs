using System.Data.SqlClient;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private const string IS_USER_EXIST_COMMAND = "P_READ_IS_USER_EXIST";
        public User GetUserByUserName(string userName)
        {
            return new User();
        }

        public bool IsUserExists(string userName)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(IS_USER_EXIST_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parameter = new SqlParameter("@USER_NAME", userName);
                    command.Parameters.Add(parameter);

                    connection.Open();
                    var result = command.ExecuteScalar();

                    connection.Close();

                    return result != null;
                }
            }
        }
    }
}
