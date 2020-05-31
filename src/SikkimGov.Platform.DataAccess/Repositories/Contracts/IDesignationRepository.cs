using System.Collections.Generic;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IDesignationRepository
    {
        List<Designation> GetAllDesignations();
    }
}
