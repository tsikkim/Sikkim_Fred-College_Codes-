using System.Collections.Generic;
using System.Linq;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class DistrictRepository : IDistrictRepository
    {
        private IDbContext dbContext;

        public DistrictRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<District> GetAllDistricts()
        {
            return this.dbContext.Districts.ToList();
        }
    }
}
