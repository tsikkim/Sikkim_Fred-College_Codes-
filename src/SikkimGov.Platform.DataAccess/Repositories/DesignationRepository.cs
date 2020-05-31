using System.Collections.Generic;
using System.Linq;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class DesignationRepository : IDesignationRepository
    {
        private IDbContext dbContext;

        public DesignationRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Designation> GetAllDesignations()
        {
            return this.dbContext.Designations.ToList();
        }
    }
}
