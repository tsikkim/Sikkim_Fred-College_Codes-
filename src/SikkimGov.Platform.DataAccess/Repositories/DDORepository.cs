using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class DDORepository : BaseRepository, IDDORepository
    {
        private const string DDOBASE_READ_COMMAND = "P_READ_DDO";
        private const string DDODETAILS_READ_COMMAND = "P_READ_DDO_DETAILS";
        private const string DDOBASE_READ_ALL_COMMAND = "P_READ_ALL_DDO";


        public List<DDOBase> GetDDOBaseByDeparmentId(int deparmentId)
        {
            var ddoList = new List<DDOBase>();
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(DDOBASE_READ_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parameter = new SqlParameter("@DEPT_ID", deparmentId);
                    command.Parameters.Add(parameter);
                    connection.Open();
                    using (var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var ddo = new DDOBase();
                            ddo.Id = reader.GetInt32(0);
                            ddo.Code = reader.GetString(1);
                            ddoList.Add(ddo);
                        }
                    }
                }
            }
            return ddoList;
        }

        public DDODetails GetDDODetailsByDDOCode(string ddoCode)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(DDODETAILS_READ_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parameter = new SqlParameter("@DDO_CODE", ddoCode);
                    command.Parameters.Add(parameter);
                    connection.Open();
                    using (var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            var ddoDetails = new DDODetails();
                            ddoDetails.Id = reader.GetInt32(0);
                            ddoDetails.DepartmentId = reader.GetByte(1);
                            ddoDetails.Code = reader.GetString(2);
                            ddoDetails.Name = reader.GetString(3);
                            ddoDetails.DesignationId = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                            ddoDetails.DesignationName = reader.IsDBNull(5) ? "" : reader.GetString(5);
                            ddoDetails.DistrictId = reader.IsDBNull(6) ? 0 : reader.GetInt16(6);
                            ddoDetails.DistrictName = reader.IsDBNull(7) ? "" : reader.GetString(7);

                            return ddoDetails;
                        }
                    }
                }
            }
            return null;
        }

        public List<DDOBase> GetAllDDOCodeBases()
        {
            var ddoList = new List<DDOBase>();
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(DDOBASE_READ_ALL_COMMAND, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var ddo = new DDOBase();
                            ddo.Id = Convert.ToInt32(reader["DDO_ID"]);
                            ddo.Code = reader["DDO_CODE"].ToString();
                            ddo.DepartmentId = Convert.ToInt32(reader["DEPT_ID"]);
                            ddoList.Add(ddo);
                        }
                    }
                }
            }
            return ddoList;
        }
    }
}
