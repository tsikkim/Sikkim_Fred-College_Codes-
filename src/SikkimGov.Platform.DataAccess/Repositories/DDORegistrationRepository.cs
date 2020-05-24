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
        private const string DDO_REG_DETAILS_READ_BY_STATUS_COMMAND = "P_DDO_REG_DETAILS_READ_BY_STATUS";
        private const string DDO_REG_DEL_COMMAND = "P_DEL_DDO_REGISTRATION";
        private const string DDO_REG_READ_BY_ID_COMMAND = @"P_DDO_REG_READ_BY_ID";
        private const string DDO_REG_UPDATE_STATUS_COMMAND = "P_DDO_REG_UPDATE_STATUS";

        public DDORegistration CreateDDORegistration(DDORegistration ddoRegistration)
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

        public List<DDORegistrationDetails> GetDDORegistrationsByStatus(bool? status)
        {
            var ddoRegistrations = new List<DDORegistrationDetails>();

            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(DDO_REG_DETAILS_READ_BY_STATUS_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parameter = new SqlParameter("@STATUS", status);
                    command.Parameters.Add(parameter);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ddoRegistration = new DDORegistrationDetails();
                            ddoRegistration.Id = Convert.ToInt64(reader["REG_ID"]);
                            ddoRegistration.DDOCode = reader["DDO_CODE"] == DBNull.Value ? "" : reader["DDO_CODE"].ToString();
                            ddoRegistration.DepartmentId = reader["DEPT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DEPT_ID"]);
                            ddoRegistration.DepartmentName = reader["DEPT_NAME"] == DBNull.Value ? "" : reader["DEPT_NAME"].ToString();
                            ddoRegistration.DistrictId = reader["DISTRICT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DISTRICT_ID"]);
                            ddoRegistration.DistrictName = reader["DISTRICT_NAME"] == DBNull.Value ? "" : reader["DISTRICT_NAME"].ToString();
                            ddoRegistration.DesignationId = reader["DESIG_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DESIG_ID"]);
                            ddoRegistration.DesginationName = reader["DESIG_DESC"] == DBNull.Value ? "" : reader["DESIG_DESC"].ToString();
                            ddoRegistration.OfficeAddress1 = reader["OFFICE_ADD_1"] == DBNull.Value ? "" : reader["OFFICE_ADD_1"].ToString();
                            ddoRegistration.OfficeAddress2 = reader["OFFICE_ADD_2"] == DBNull.Value ? "" : reader["OFFICE_ADD_2"].ToString();
                            ddoRegistration.TINNumber = reader["TIN_NO"] == DBNull.Value ? "" : reader["TIN_NO"].ToString();
                            ddoRegistration.TANNumber = reader["TAN_NO"] == DBNull.Value ? "" : reader["TAN_NO"].ToString();
                            ddoRegistration.EmailId = reader["EMAIL"] == DBNull.Value ? "" : reader["EMAIL"].ToString();
                            ddoRegistration.ContactNumber = reader["CONTACT_NO"] == DBNull.Value ? "" : reader["CONTACT_NO"].ToString();
                            ddoRegistration.Status = Convert.ToBoolean(reader["CUR_STATUS"]);
                            ddoRegistration.StatusName = reader["STATUS_NAME"].ToString();
                            ddoRegistration.CreateAt = Convert.ToDateTime(reader["ENTRY_TIME"]);
                            ddoRegistration.ApprovedBy = reader["PASSED_BY"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PASSED_BY"]);
                            ddoRegistration.ApprovedDate = null;

                            if(reader["PASSING_TIME"] != DBNull.Value)
                            {
                                ddoRegistration.ApprovedDate = Convert.ToDateTime(reader["PASSING_TIME"]);
                            }

                            ddoRegistrations.Add(ddoRegistration);
                        }
                    }

                    return ddoRegistrations;
                }
            }
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
                        if (reader.Read())
                        {
                            var ddoRegistration = new DDORegistration();
                            ddoRegistration.Id = Convert.ToInt64(reader["REG_ID"]);
                            ddoRegistration.DDOCode = reader["DDO_CODE"].ToString();
                            ddoRegistration.DepartmentId = Convert.ToInt32(reader["DEPT_ID"]);
                            ddoRegistration.DesignationId = Convert.ToInt32(reader["DESIG_ID"]);
                            ddoRegistration.DistrictId = Convert.ToInt32(reader["DISTRICT_ID"]);
                            ddoRegistration.OfficeAddress1 = reader["OFFICE_ADD_1"].ToString();
                            ddoRegistration.OfficeAddress2 = reader["OFFICE_ADD_2"] == DBNull.Value ? "" : reader["OFFICE_ADD_2"].ToString();
                            ddoRegistration.TINNumber = reader["TIN_NO"] == DBNull.Value ? "" : reader["TIN_NO"].ToString();
                            ddoRegistration.TANNumber = reader["TAN_NO"].ToString();
                            ddoRegistration.EmailId = reader["EMAIL"].ToString();
                            ddoRegistration.ContactNumber = reader["CONTACT_NO"].ToString();
                            ddoRegistration.Status = Convert.ToBoolean(reader["CUR_STATUS"]);
                            ddoRegistration.CreateAt = Convert.ToDateTime(reader["ENTRY_TIME"]);
                            ddoRegistration.ApprovedBy = reader["PASSED_BY"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PASSED_BY"]);
                            ddoRegistration.ApprovedDate = null;
                            if (!reader.IsDBNull(13))
                            {
                                ddoRegistration.ApprovedDate = Convert.ToDateTime(reader["PASSING_TIME"]);
                            }

                            return ddoRegistration;
                        }
                    }
                }
            }

            return null;
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
