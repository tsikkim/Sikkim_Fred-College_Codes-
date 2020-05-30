using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SikkimGov.Platform.Models.Domain
{
    [Table("SBSReceipt")]
    public class SBSReceipt
    {
        public long ID { get; set; }

        public string BranchCode { get; set; }

        public string BranchName { get; set; }

        public string BranchChallanNo { get; set; }

        public DateTime TransactionDate { get; set; }

        public string MajorHead { get; set; }

        public string ReceiptPayment { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
