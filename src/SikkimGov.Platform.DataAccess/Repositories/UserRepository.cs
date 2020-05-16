using System;
using System.Data.SqlClient;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private const string IS_USER_EXIST_COMMAND = "P_READ_IS_USER_EXIST";
        private const string USER_SAVE_COMMAND = "P_INS_USER";
        private const string USER_DEL_COMMAND = "P_DEL_USER";
        private const string USER_DEL_BY_USER_NAME_COMMAND = "P_DEL_USER_BY_USER_NAME";
        private const string USER_UPDATE_STATUS_BY_USER_NAME_COMMAND = "P_USER_UPDATE_STATUS_BY_USER_NAME";

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

        public User SaveUser(User user)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(USER_SAVE_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parameter = new SqlParameter("@USER_ID", DBNull.Value);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@USER_NAME", user.UserName);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@PASSWORD", user.Password);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@IS_LOGGED_IN", user.IsLoggedIn);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@TYPE", user.UserType);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@IS_SUPER", user.IsSuperAdmin);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@IS_ADMIN", user.IsAdmin);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@IS_RCO", user.IsRCOUser);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@IS_DDO", user.IsDDOUser);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@DEPT", user.DepartmentId);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@ddocode", user.DDOCode);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@RETURN_ID", System.Data.SqlDbType.BigInt);
                    parameter.Direction = System.Data.ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    connection.Open();

                    command.ExecuteNonQuery();

                    user.Id = Convert.ToInt64(command.Parameters["@RETURN_ID"].Value);

                }
            }
            return user;
        }

        public void DeleteUser(long userId)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(USER_DEL_COMMAND, connection))
                {
                    var parameter = new SqlParameter("@USER_ID", userId);
                    command.Parameters.Add(parameter);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void DeleteUserByUserName(string userName)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(USER_DEL_BY_USER_NAME_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = new SqlParameter("@USER_NAME", userName);
                    command.Parameters.Add(parameter);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public bool UpdateUserStatusByUserName(string userName, bool status)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(USER_UPDATE_STATUS_BY_USER_NAME_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = new SqlParameter("@USER_NAME", userName);
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter("@STATUS", status);
                    command.Parameters.Add(parameter);

                    connection.Open();
                    var rowCount = command.ExecuteNonQuery();
                    connection.Close();

                    return rowCount > 0;
                }
            }
        }
    }
}