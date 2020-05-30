using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Internal;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.Domain;

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

        private readonly IDbContext dbContext;

        public UserRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool IsUserExists(string userName)
        {
            return this.dbContext.Users.Any(user => user.UserName == userName);
        }

        public User GetUserByUsername(string userName)
        {
            return this.dbContext.Users.FirstOrDefault(user => user.UserName == userName);
        }

        public User SaveUser(User user)
        {
            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();
            return user;
        }

        public bool DeleteUser(User user)
        {
            this.dbContext.Users.Remove(user);
            var result = this.dbContext.SaveChanges();
            return result != 0;
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

        public List<Models.DomainModels.UserDetails> GetDDOUserDetails()
        {
            //DDO_USER_DETAILS_READ_COMMAND
            return new List<Models.DomainModels.UserDetails>();
        }

        public List<Models.DomainModels.UserDetails> GetRCOUserDetails()
        {
            //RCO_USER_DETAILS_READ_COMMAND
            var userDetailsList = new List<Models.DomainModels.UserDetails>();

            return userDetailsList;
        }

        public List<Models.DomainModels.UserDetails> GetAdminUserDetails()
        {
            //ADMIN_USER_DETAILS_READ_COMMAND
            var userDetailsList = new List<Models.DomainModels.UserDetails>();

            return userDetailsList;
        }
    }
}