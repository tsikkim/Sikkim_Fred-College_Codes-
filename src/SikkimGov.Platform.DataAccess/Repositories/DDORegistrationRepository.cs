using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class DDORegistrationRepository : BaseRepository, IDDORegistrationRepository
    {
        private const string DDO_REG_SAVE_COMMAND = "P_DDO_REGISTRATION_INS";
        private const string DDO_REG_DETAILS_READ_BY_STATUS_COMMAND = "P_DDO_REG_DETAILS_READ_BY_STATUS";
        private const string DDO_REG_DEL_COMMAND = "P_DEL_DDO_REGISTRATION";
        private const string DDO_REG_READ_BY_ID_COMMAND = @"P_DDO_REG_READ_BY_ID";
        private const string DDO_REG_UPDATE_STATUS_COMMAND = "P_DDO_REG_UPDATE_STATUS";

        private readonly IDbContext dbContext;

        public DDORegistrationRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DDORegistration CreateDDORegistration(DDORegistration ddoRegistration)
        {
            this.dbContext.DDORegistrations.Add(ddoRegistration);
            this.dbContext.SaveChanges();
            return ddoRegistration;
        }

        public List<Models.DomainModels.DDORegistrationDetails> GetDDORegistrationsByStatus(bool? status)
        {
            var query = from ddoReg in this.dbContext.DDORegistrations
                        join ddoInfo in this.dbContext.DDOInfos
                            on ddoReg.DDOCode equals ddoInfo.DDOCode into ddo
                        from ddoCode in ddo.DefaultIfEmpty()
                        join department in this.dbContext.Departments
                            on ddoReg.DepartmentID equals department.DepartmentId into deptTemp
                        from dept in deptTemp.DefaultIfEmpty()
                        join district in this.dbContext.Districts
                            on ddoReg.DistrictID equals district.DistrictId into districtTemp
                        from dist in districtTemp.DefaultIfEmpty()
                        join designation in this.dbContext.Designations
                            on ddoReg.DesignationID equals designation.DesignationId into designationTemp
                        from desgn in designationTemp.DefaultIfEmpty()
                        select new Models.DomainModels.DDORegistrationDetails
                        {
                            Id = ddoReg.RegistrationID,
                            DDOCode = ddoCode.DDOCode,
                            DepartmentId = ddoReg.DepartmentID,
                            DepartmentName = dept.DepartmentName,
                            DistrictId = ddoReg.DistrictID,
                            DistrictName = dist.DistrictName,
                            DesignationId = ddoReg.DesignationID,
                            DesginationName = desgn.DesignationName,
                            OfficeAddress1 = ddoReg.OfficeAddress1,
                            OfficeAddress2 = ddoReg.OfficeAddress2,
                            TINNumber = ddoReg.TINNumber,
                            TANNumber = ddoReg.TANNumber,
                            EmailId = ddoReg.EmailID,
                            ContactNumber = ddoReg.ContactNumber,
                            Status = ddoReg.IsApproved,
                            StatusName = ddoReg.IsApproved ? "APPROVED" : "PENDING",
                            CreateAt = ddoReg.CreatedDate,
                            ApprovedBy = ddoReg.ApprovedBy,
                            ApprovedDate = ddoReg.ApprovedDate
                        };

            if(status.HasValue)
            {
                query = query.Where(item => item.Status == status);
            }

            return query.ToList();
        }

        public bool DeleteDDORegistration(DDORegistration ddoRegistration)
        {
            this.dbContext.DDORegistrations.Remove(ddoRegistration);

            var result = this.dbContext.SaveChanges();

            return result != 0;
        }

        public DDORegistration GetDDORegistrationById(long ddoRegistrationId)
        {
            return this.dbContext.DDORegistrations.FirstOrDefault(registration => registration.RegistrationID == ddoRegistrationId);
        }

        public DDORegistration UpdateRegistration(DDORegistration ddoRegistration)
        {
            this.dbContext.DDORegistrations.Update(ddoRegistration);
            this.dbContext.SaveChanges();
            return ddoRegistration;
        }

        public bool UpdateDDORegistrationStatus(long ddoRegistrationId, bool status, int updatedBy)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(DDO_REG_UPDATE_STATUS_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parameter = new SqlParameter("@DDO_REG_ID", ddoRegistrationId);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@STATUS", status);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@UPDATEDBY", updatedBy);
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
