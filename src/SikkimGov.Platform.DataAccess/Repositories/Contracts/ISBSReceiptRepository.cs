using System.Collections.Generic;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface ISBSReceiptRepository
    {
        List<SBSReceipt> GetAllSBSReceipts();

        void AddSBSReceipts(IEnumerable<SBSReceipt> sbsReceipts);
    }
}