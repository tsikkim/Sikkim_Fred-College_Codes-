using System.Collections.Generic;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IDDORepository 
    {
        List<DDOBase> GetDDOBaseByDeparmentId(int deparmentId);
        DDODetails GetDDODetailsByDDOCode(string ddoCode);
    }
}
