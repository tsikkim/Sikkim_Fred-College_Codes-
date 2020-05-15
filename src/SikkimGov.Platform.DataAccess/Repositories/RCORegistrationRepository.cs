using System;
using System.Data.SqlClient;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class RCORegistrationRepository : BaseRepository, IRCORegistrationRepository
    {
        private const string RCO_REG_SAVE_COMMAND = "P_RCO_Registration_INS";

        public RCORegistration SaveRCORegistration(RCORegistration rcoRegistration)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(RCO_REG_SAVE_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parameter = new SqlParameter("@RCO_Name_Of_Administrator", rcoRegistration.AdminName);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@Type_Of_Reg", rcoRegistration.RegistrationType);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@RCO_Department", rcoRegistration.Department);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@RCO_Designation", rcoRegistration.Designation);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@RCO_District", rcoRegistration.District);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@RCO_Office_Add1", rcoRegistration.OfficeAddress1);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@RCO_Office_Add2", string.IsNullOrEmpty(rcoRegistration.OfficeAddress2) ? "" : rcoRegistration.OfficeAddress2);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@RCO_Tin_No", string.IsNullOrEmpty(rcoRegistration.TINNumber) ? "" : rcoRegistration.TINNumber);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@RCO_Tan_No", rcoRegistration.TANNumber);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@RCO_Email", rcoRegistration.EmailId);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@RCO_Contact", rcoRegistration.ContactNumber);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@RCO_Entry_Time", DBNull.Value);
                    command.Parameters.Add(parameter);
                    parameter = new SqlParameter("@RETURN_ID", System.Data.SqlDbType.BigInt);
                    parameter.Direction = System.Data.ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    connection.Open();

                    command.ExecuteNonQuery();

                    var newId = Convert.ToInt64(command.Parameters["@RETURN_ID"].Value);

                    rcoRegistration.Id = newId;
                }
            }
            return rcoRegistration;
        }
    }
}