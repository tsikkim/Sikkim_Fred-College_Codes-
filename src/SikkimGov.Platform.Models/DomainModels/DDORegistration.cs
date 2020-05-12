using System;

namespace SikkimGov.Platform.Models.DomainModels
{
    public class DDORegistration
    {
        public long Id { get; set; }
        public string DDOCode { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int DistrictId { get; set; }
        public string OfficeAddress1 { get; set; }
        public string OfficeAddress2 { get; set; }
        public string TINNumber { get; set; }
        public string TANNumber { get; set; }
        public string EmailId { get; set; }
        public string ContactNumber { get; set; }
        public bool Status { get; set; }
        public DateTime CreateAt { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime ApprovedAt { get; set; }
    }
}