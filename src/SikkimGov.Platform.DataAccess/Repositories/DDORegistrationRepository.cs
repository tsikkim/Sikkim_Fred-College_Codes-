using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class DDORegistrationRepository : BaseRepository, IDDORegistrationRepository
    {
        private const string DDO_REG_SAVE_COMMAND = "P_DDO_REGISTRATION_INS";

        private const string DDO_REG_GET_BY_STATUS_COMMAND = "P_DDO_REGISTRATION_GET_BY_STATUS";

        private const string DDO_REG_DEL_COMMAND = "P_DEL_DDO_REGISTRATION";

        private const string DDO_REG_READ_BY_ID_COMMAND = @"P_DDO_REG_READ_BY_ID";

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
                    parameter = new SqlParameter("@TIN_NO", string.IsNullOrEmpty(ddoRegistration.TINNumber) ? "" : ddoRegistration.TINNumber);
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

        public List<DDORegistration> GetDDORegistrationsByStatus(bool status)
        {
            return new List<DDORegistration>();
        }

        public bool DeleteDDORegistration(long ddoRegistrationId)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(DDO_REG_DEL_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parameter = new SqlParameter("@DDO_REG_ID", ddoRegistrationId);
                    command.Parameters.Add(parameter);

                    connection.Open();
                    var rowCount = command.ExecuteNonQuery();
                    connection.Close();
                    return rowCount > 0;
                }
            }
        }

        public DDORegistration GetDDORegistrationById(long ddoRegistrationId)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(DDO_REG_READ_BY_ID_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parameter = new SqlParameter("@DDO_REG_ID", ddoRegistrationId);
                    command.Parameters.Add(parameter);

                    connection.Open();

                    using (var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        if(reader.Read())
                        {
                            var ddoRegistration = new DDORegistration();
                            ddoRegistration.Id = reader.GetInt64(0);
                            ddoRegistration.DDOCode = reader.GetString(1);
                            ddoRegistration.DepartmentId = reader.GetInt32(2);
                            ddoRegistration.DesignationId = reader.GetInt32(3);
                            ddoRegistration.DistrictId = reader.GetInt32(4);
                            ddoRegistration.OfficeAddress1 = reader.GetString(5);
                            ddoRegistration.OfficeAddress2 = reader.IsDBNull(6) ? "" : reader.GetString(6);
                            ddoRegistration.TINNumber = reader.IsDBNull(7) ? "" : reader.GetString(7);
                            ddoRegistration.TANNumber = reader.GetString(8);
                            ddoRegistration.EmailId = reader.GetString(9);
                            ddoRegistration.ContactNumber = reader.GetString(10);
                            ddoRegistration.Status = reader.GetBoolean(11);
                            ddoRegistration.ApprovedBy = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                            ddoRegistration.ApprovedAt = null;
                            if(!reader.IsDBNull(13))
                            {
                                ddoRegistration.ApprovedAt = reader.GetDateTime(13);
                            }

                            return ddoRegistration;
                        }
                    }
                }
            }

            return null;
        }
    }
}
