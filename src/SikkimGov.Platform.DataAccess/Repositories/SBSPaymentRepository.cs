using System.Collections.Generic;
using System.Linq;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class SBSPaymentRepository : BaseRepository, ISBSPaymentRepository
    {
        private readonly IDbContext dbContext;
        public SBSPaymentRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public List<SBSPayment> GetSBSPayments()
        {
            return new List<SBSPayment>();
        }

        public void AddSBSPaymets(IEnumerable<SBSPayment> sbsPayments)
        {
            this.dbContext.AddEntities(sbsPayments.ToList());
        }
    } 
}
