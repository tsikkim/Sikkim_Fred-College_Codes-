using System;

namespace SikkimGov.Platform.Models.DomainModels
{
    public class SBSPayment
    {
        public long Id { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public int DepartmentId { get; set; }
        public DateTime ChequeDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public long ChequeNumber { get; set; }
        public decimal ChequeAmount { get; set; }
        public string PlanType { get; set; }
        public string MajorHead { get; set; }
        public int PAOCode { get; set; }
        public string IsWorks { get; set; }
    }
}
