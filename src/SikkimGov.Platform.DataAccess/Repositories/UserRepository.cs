using System.Collections.Generic;
using System.Linq;
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

        public bool IsUserExists(string emailId)
        {
            return this.dbContext.Users.Any(user => user.EmailID == emailId);
        }

        public User GetUserByEmailId(string emailId)
        {
            return this.dbContext.Users.FirstOrDefault(user => user.EmailID == emailId);
        }

        public User SaveUser(User user)
        {
            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            this.dbContext.Users.Update(user);
            this.dbContext.SaveChanges();
            return user;
        }

        public bool DeleteUser(User user)
        {
            this.dbContext.Users.Remove(user);
            var result = this.dbContext.SaveChanges();
            return result != 0;
        }

        public List<Models.DomainModels.UserDetails> GetDDOUserDetails()
        {
            var query = from user in this.dbContext.Users
                        join ddoReg in this.dbContext.DDORegistrations
                            on user.EmailID equals ddoReg.EmailID
                        join ddoInfo in this.dbContext.DDOInfos
                            on ddoReg.DDOCode equals ddoInfo.DDOCode into ddoTemp
                        from ddo in ddoTemp.DefaultIfEmpty()
                        join dept in this.dbContext.Departments
                            on ddoReg.DepartmentID equals dept.Id into deptTemp
                        from department in deptTemp.DefaultIfEmpty()
                        where user.IsActive && user.UserType == UserType.DDOUser
                        select new Models.DomainModels.UserDetails
                        {
                            Id = user.UserID,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            DDOCode = ddo.DDOCode,
                            DepartmentName = department.Name,
                            EmailId = user.EmailID,
                            UserType = user.UserType.ToString(),
                            IsDDOUser = user.UserType == UserType.DDOUser,
                            IsRCOUser = user.UserType == UserType.RCOUser,
                            IsAdmin = user.UserType == UserType.Admin,
                            IsSuperAdmin = user.UserType == UserType.SuperAdmin,
                            Name = user.FirstName + " " + user.LastName
                        };

            return query.ToList();
        }

        public List<Models.DomainModels.UserDetails> GetRCOUserDetails()
        {
            var query = from user in this.dbContext.Users
                        join rcoReg in this.dbContext.RCORegistrations
                            on user.EmailID equals rcoReg.EmailID
                        join dept in this.dbContext.Departments
                            on rcoReg.DepartmentID equals dept.Id into deptTemp
                        from department in deptTemp.DefaultIfEmpty()
                        where user.IsActive && user.UserType == UserType.RCOUser
                        select new Models.DomainModels.UserDetails
                        {
                            Id = user.UserID,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            DepartmentName = department.Name,
                            EmailId = user.EmailID,
                            UserType = user.UserType.ToString(),
                            IsDDOUser = user.UserType == UserType.DDOUser,
                            IsRCOUser = user.UserType == UserType.RCOUser,
                            IsAdmin = user.UserType == UserType.Admin,
                            IsSuperAdmin = user.UserType == UserType.SuperAdmin,
                            Name = user.FirstName + " " + user.LastName,
                            RegistrationType = rcoReg.RegistrationType
                        };

            return query.ToList();
        }

        public List<Models.DomainModels.UserDetails> GetAdminUserDetails()
        {
            var query = from user in this.dbContext.Users
                        join dept in this.dbContext.Departments
                            on user.DepartmentID equals dept.Id into deptTemp
                        from department in deptTemp.DefaultIfEmpty()
                        join dist in this.dbContext.Districts
                            on user.DistrictID equals dist.Id into distTemp
                        from district in distTemp.DefaultIfEmpty()
                        join desig in this.dbContext.Designations
                            on user.DepartmentID equals desig.Id into desigTemp
                        from designation in desigTemp.DefaultIfEmpty()
                        where user.IsActive && (user.UserType == UserType.Admin || user.UserType == UserType.SuperAdmin)
                        select new Models.DomainModels.UserDetails
                        {
                            Id = user.UserID,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            DepartmentName = department.Name,
                            EmailId = user.EmailID,
                            UserType = user.UserType.ToString(),
                            IsDDOUser = user.UserType == UserType.DDOUser,
                            IsRCOUser = user.UserType == UserType.RCOUser,
                            IsAdmin = user.UserType == UserType.Admin,
                            IsSuperAdmin = user.UserType == UserType.SuperAdmin,
                            Name = user.FirstName + " " + user.LastName
                        };

            return query.ToList();
        }
    }
}