using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SikkimGov.Platform.Models.Domain
{
    [Table("SBSPayment")]
    public class SBSPayment
    {
        public long ID { get; set; }

        public string BranchCode { get; set; }

        public string BranchName { get; set; }

        public int DepartmentID { get; set; }
        
        public DateTime ChequeDate { get; set; }
        
        public DateTime PaymentDate { get; set; }
        
        public string ChequeNumber { get; set; }
        
        public decimal ChequeAmount { get; set; }
        
        public string PlanNonPlan { get; set; }
        
        public string MajorHead { get; set; }
        
        public int PAOCode { get; set; }
        
        public string IsWorks { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
