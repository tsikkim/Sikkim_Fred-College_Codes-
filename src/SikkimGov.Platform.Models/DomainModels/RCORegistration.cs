using System;

namespace SikkimGov.Platform.Models.DomainModels
{
    public class RCORegistration
    {
        public long Id { get; set; }
        public string AdminName { get; set; }
        public string RegistrationType { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string District { get; set; }
        public string OfficeAddress1 { get; set; }
        public string OfficeAddress2 { get; set; }
        public string TINNumber { get; set; }
        public string TANNumber { get; set; }
        public string EmailId { get; set; }
        public string ContactNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}
