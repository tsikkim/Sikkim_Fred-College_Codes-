using System;

namespace SikkimGov.Platform.Models.DomainModels
{
    public class SBSReceipt
    {
        public long Id { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string ChallanNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string MajorHead { get; set; }
        public string ReceiptPayment { get; set; }
        public decimal Amount { get; set; }
    }
}
