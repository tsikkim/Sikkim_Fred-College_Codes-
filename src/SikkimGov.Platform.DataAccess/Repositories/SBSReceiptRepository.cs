using System.Collections.Generic;
using System.Linq;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.ApiModels;
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

        public List<AmountPerMonth> GetAmountPerMonth(int startMonth, int startYear, int endMonth, int endYear)
        {
            var query = from payment in this.dbContext.SBSReceipts
                        group payment by new { payment.TransactionDate.Month, payment.TransactionDate.Year }
                       into amountPerMonth
                        where amountPerMonth.Key.Month >= startMonth && amountPerMonth.Key.Year >= startYear
                        && amountPerMonth.Key.Month <= endMonth && amountPerMonth.Key.Year <= endYear
                        orderby amountPerMonth.Key.Year, amountPerMonth.Key.Month
                        select new AmountPerMonth 
                        { 
                            Month = $"{amountPerMonth.Key.Month}-{amountPerMonth.Key.Year}", 
                            Amount = amountPerMonth.Sum(item => item.Amount) 
                        };

            return query.ToList();
        }

        public List<AmountPerMonth> GetAmountPerMonthForYearRange(int startYear, int endYear)
        {
            var query = from payment in this.dbContext.SBSReceipts
                        group payment by new { payment.TransactionDate.Month, payment.TransactionDate.Year }
                        into amountPerMonth
                        where amountPerMonth.Key.Year >= startYear && amountPerMonth.Key.Year <= endYear
                        orderby amountPerMonth.Key.Year, amountPerMonth.Key.Month
                        select new AmountPerMonth 
                        { 
                            Month = $"{amountPerMonth.Key.Month}-{amountPerMonth.Key.Year}", 
                            Amount = amountPerMonth.Sum(item => item.Amount) 
                        };

            return query.ToList();
        }
    }
}