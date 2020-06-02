using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
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

        private readonly IDbContext dbContext;

        public DDORepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<DDOBase> GetDDOBaseByDeparmentId(int deparmentId)
        {
            var ddoList = dbContext.DDOInfos.Where(ddo => ddo.DepartmentID == deparmentId)
                .Select(ddo => new DDOBase { Id = ddo.ID, Code = ddo.DDOCode, DepartmentId = deparmentId }).ToList();

            return ddoList;
        }

        public DDODetails GetDDODetailsByDDOCode(string ddoCode)
        {
            var query = from ddo in this.dbContext.DDOInfos
                    join district in this.dbContext.Districts
                    on ddo.DistrictID equals district.Id
                    where ddo.DDOCode == ddoCode
                    select new DDODetails 
                    {
                        Id = ddo.ID,
                        DepartmentId = ddo.DepartmentID,
                        Code = ddo.DDOCode,
                        Name = ddo.Name,
                        DesignationId = ddo.DesignationID,
                        DistrictId = ddo.DistrictID,
                        DistrictName = district.Name
                    };

            return query.FirstOrDefault();
        }

        public List<DDOBase> GetAllDDOCodeBases()
        {
            var ddoList = this.dbContext.DDOInfos
                            .Select(ddo => new DDOBase 
                            { 
                                Id = ddo.ID, 
                                Code = ddo.DDOCode, 
                                DepartmentId = ddo.DepartmentID 
                            }).ToList();

            return ddoList;
        }
    }
}
