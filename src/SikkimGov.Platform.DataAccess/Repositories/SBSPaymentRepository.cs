using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.ApiModels;
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

        public List<AmountPerMonth> GetAmountPerMonth(int startMonth, int startYear, int endMonth, int endYear)
        {
            var query = from payment in this.dbContext.SBSPayments
                        group payment by new { payment.PaymentDate.Month, payment.PaymentDate.Year }
                        into amountPerMonth
                        where amountPerMonth.Key.Month >= startMonth && amountPerMonth.Key.Year >= startYear
                        && amountPerMonth.Key.Month <= endMonth && amountPerMonth.Key.Year <= endYear
                        orderby amountPerMonth.Key.Year, amountPerMonth.Key.Month
                        select new AmountPerMonth { Month = $"{amountPerMonth.Key.Month}-{amountPerMonth.Key.Year}", Amount = amountPerMonth.Sum(item => item.ChequeAmount) };

            return query.ToList();
        }

        public List<AmountPerMonth> GetAmountPerMonthForYear(int year)
        {
            var query = from payment in this.dbContext.SBSPayments
                        group payment by new { payment.PaymentDate.Month, payment.PaymentDate.Year }
                        into amountPerMonth
                        where amountPerMonth.Key.Year == year
                        orderby amountPerMonth.Key.Year, amountPerMonth.Key.Month
                        select new AmountPerMonth 
                        { 
                            Month = $"{amountPerMonth.Key.Month}-{amountPerMonth.Key.Year}",
                            Amount = amountPerMonth.Sum(item => item.ChequeAmount)
                        };

            return query.ToList();
        }

        public List<AmountPerMonth> GetAmountPerMonthForYearRange(int startYear, int endYear)
        {
            var query = from payment in this.dbContext.SBSPayments
                        group payment by new { payment.PaymentDate.Month, payment.PaymentDate.Year }
                        into amountPerMonth
                        where amountPerMonth.Key.Year >= startYear && amountPerMonth.Key.Year <= endYear
                        orderby amountPerMonth.Key.Year, amountPerMonth.Key.Month
                        select new AmountPerMonth 
                        {
                            Month = $"{amountPerMonth.Key.Month}-{amountPerMonth.Key.Year}",
                            Amount = amountPerMonth.Sum(item => item.ChequeAmount)
                        };

            return query.ToList();
        }
    } 
}