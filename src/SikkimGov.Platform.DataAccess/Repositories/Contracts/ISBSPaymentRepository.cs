using System.Collections.Generic;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface ISBSPaymentRepository
    {
        List<SBSPayment> GetSBSPayments();

        void AddSBSPaymets(IEnumerable<SBSPayment> sbsPayments);
    }
}
