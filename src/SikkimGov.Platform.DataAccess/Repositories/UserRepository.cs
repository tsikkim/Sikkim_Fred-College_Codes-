using System;
using System.Collections.Generic;
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
        private const string USER_READ_BY_USER_NAME_COMMAND = "P_READ_USER_BY_USER_NAME";
        private const string DDO_USER_DETAILS_READ_COMMAND = "P_READ_DDO_USER_DETAILS";
        private const string RCO_USER_DETAILS_READ_COMMAND = "P_READ_RCO_USER_DETAILS";
        private const string ADMIN_USER_DETAILS_READ_COMMAND = "P_READ_ADMIN_USER_DETAILS";

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

        public User GetUserByUsername(string userName)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(USER_READ_BY_USER_NAME_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parameter = new SqlParameter("@USER_NAME", userName);
                    command.Parameters.Add(parameter);

                    connection.Open();
                    using (var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            var user = new User();
                            user.Id = Convert.ToInt64(reader["USER_ID"]);
                            user.UserName = reader["USER_NAME"].ToString();
                            user.Password = reader["PASSWORD"].ToString();
                            user.IsLoggedIn = Convert.ToBoolean(reader["IS_LOGGED_IN"]);
                            user.LastLoginDate = Convert.ToDateTime(reader["LAST_LOGIN_DATE"]);
                            user.IsActive = Convert.ToBoolean(reader["ACTIVE"]);
                            user.CreatedDate = Convert.ToDateTime(reader["CREATE_DATE"]);
                            user.LastSchoolNumber = Convert.ToInt64(reader["LAST_SCH_NO"]);
                            user.Step = Convert.ToByte(reader["STEP"]);
                            user.UserType = Convert.ToByte(reader["TYPE"]);
                            user.IsAdmin = reader["IS_ADMIN"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_ADMIN"]);
                            user.DepartmentId = reader["DEPT_ID"] == DBNull.Value ? 0 : Convert.ToInt64(reader["DEPT_ID"]);
                            user.IsDDOUser = reader["IS_DDO"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_DDO"]);
                            user.IsSuperAdmin = reader["IS_SUPER"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_SUPER"]);
                            user.EmailId = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();
                            user.IsRCOUser = reader["IS_RCO"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_RCO"]);
                            user.DDOCode = reader["ddocode"] == DBNull.Value ? "" : reader["ddocode"].ToString();
                            return user;
                        }
                    }
                }
            }

            return null;
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

        public List<UserDetails> GetDDOUserDetails()
        {
            var userDetailsList = new List<UserDetails>();
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(DDO_USER_DETAILS_READ_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var userDetails = new UserDetails();
                            userDetails.Id = Convert.ToInt64(reader["USER_ID"]);
                            userDetails.UserName = reader["USER_NAME"].ToString();
                            userDetails.IsAdmin = reader["IS_ADMIN"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_ADMIN"]);
                            userDetails.DepartmentName = reader["DEPT_NAME"] == DBNull.Value ? "" : reader["DEPT_NAME"].ToString();
                            userDetails.IsDDOUser = reader["IS_DDO"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_DDO"]);
                            userDetails.IsSuperAdmin = reader["IS_SUPER"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_SUPER"]);
                            userDetails.IsRCOUser = reader["IS_RCO"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_RCO"]);
                            userDetails.DDOCode = reader["ddocode"] == DBNull.Value ? "" : reader["ddocode"].ToString();
                            userDetails.Name = reader["DDO_NAME"] == DBNull.Value ? "" : reader["DDO_NAME"].ToString();

                            userDetailsList.Add(userDetails);
                        }
                    }
                }
            }

            return userDetailsList;
        }

        public List<UserDetails> GetRCOUserDetails()
        {
            var userDetailsList = new List<UserDetails>();
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(RCO_USER_DETAILS_READ_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var userDetails = new UserDetails();
                            userDetails.Id = Convert.ToInt64(reader["USER_ID"]);
                            userDetails.UserName = reader["USER_NAME"].ToString();
                            userDetails.IsAdmin = reader["IS_ADMIN"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_ADMIN"]);
                            userDetails.DepartmentName = reader["DEPT_NAME"] == DBNull.Value ? "" : reader["DEPT_NAME"].ToString();
                            userDetails.IsDDOUser = reader["IS_DDO"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_DDO"]);
                            userDetails.IsSuperAdmin = reader["IS_SUPER"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_SUPER"]);
                            userDetails.IsRCOUser = reader["IS_RCO"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_RCO"]);
                            userDetails.DDOCode = reader["ddocode"] == DBNull.Value ? "" : reader["ddocode"].ToString();

                            userDetailsList.Add(userDetails);
                        }
                    }
                }
            }

            return userDetailsList;
        }

        public List<UserDetails> GetAdminUserDetails()
        {
            var userDetailsList = new List<UserDetails>();
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(ADMIN_USER_DETAILS_READ_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var userDetails = new UserDetails();
                            userDetails.Id = Convert.ToInt64(reader["USER_ID"]);
                            userDetails.UserName = reader["USER_NAME"].ToString();
                            userDetails.IsAdmin = reader["IS_ADMIN"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_ADMIN"]);
                            userDetails.IsDDOUser = reader["IS_DDO"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_DDO"]);
                            userDetails.IsSuperAdmin = reader["IS_SUPER"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_SUPER"]);
                            userDetails.IsRCOUser = reader["IS_RCO"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_RCO"]);
                            userDetailsList.Add(userDetails);
                        }
                    }
                }
            }

            return userDetailsList;
        }
    }
}