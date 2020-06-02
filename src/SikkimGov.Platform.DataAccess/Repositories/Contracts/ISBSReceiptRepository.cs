using System.Collections.Generic;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface ISBSReceiptRepository
    {
        List<SBSReceipt> GetAllSBSReceipts();

        void AddSBSReceipts(IEnumerable<SBSReceipt> sbsReceipts);
        List<AmountPerMonth> GetAmountPerMonth(int startMonth, int startYear, int endMonth, int endYear);
        List<AmountPerMonth> GetAmountPerMonthForYearRange(int startYear, int endYear);
    }
}