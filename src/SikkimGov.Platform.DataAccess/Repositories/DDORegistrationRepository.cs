using System;
using System.Data.SqlClient;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class DDORegistrationRepository : BaseRepository, IDDORegistrationRepository
    {
        private const string DDO_REG_SAVE_COMMAND = "P_DDO_REGISTRATION_INS";

        public DDORegistration SaveDDORegistration(DDORegistration ddoRegistration)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(DDO_REG_SAVE_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parameter = new SqlParameter("@DDO_CODE", ddoRegistration.DDOCode);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@DEPT_ID", ddoRegistration.DepartmentId);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@DESIG_ID", ddoRegistration.DesignationId);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@DISTRICT_ID", ddoRegistration.DistrictId);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@OFFICE_ADD_1", ddoRegistration.OfficeAddress1);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@OFFICE_ADD_2", ddoRegistration.OfficeAddress2);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@TIN_NO", ddoRegistration.TINNumber);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@TAN_NO", ddoRegistration.TANNumber);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@EMAIL", ddoRegistration.EmailId);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@CONTACT_N0", ddoRegistration.ContactNumber);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@RETURN_ID", System.Data.SqlDbType.BigInt);
                    parameter.Direction = System.Data.ParameterDirection.Output;
                    command.Parameters.Add(parameter);
                    connection.Open();

                    command.ExecuteNonQuery();

                    ddoRegistration.Id = Convert.ToInt64(command.Parameters["@RETURN_ID"].Value);

                }
            }
            return ddoRegistration;
        }
    }
}
