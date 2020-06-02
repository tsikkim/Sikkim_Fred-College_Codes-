using System.Collections.Generic;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface ISBSPaymentRepository
    {
        List<SBSPayment> GetSBSPayments();

        void AddSBSPaymets(IEnumerable<SBSPayment> sbsPayments);

        List<AmountPerMonth> GetAmountPerMonth(int startMonth, int startYear, int endMonth, int endYear);

        List<AmountPerMonth> GetAmountPerMonthForYear(int year);

        List<AmountPerMonth> GetAmountPerMonthForYearRange(int startYear, int endYear);
    }
}
