using System.Collections.Generic;
using System.Linq;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class SBSReceiptRepository: BaseRepository, ISBSReceiptRepository
    {
        private IDbContext dbContext;
        public SBSReceiptRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<SBSReceipt> GetAllSBSReceipts()
        {
            return new List<SBSReceipt>();
        }

        public void AddSBSReceipts(IEnumerable<SBSReceipt> sbsReceipts)
        {
            this.dbContext.AddEntities(sbsReceipts.ToList());
        }
    }
}
