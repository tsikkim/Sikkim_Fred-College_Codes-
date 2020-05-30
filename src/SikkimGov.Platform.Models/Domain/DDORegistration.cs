using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SikkimGov.Platform.Models.Domain
{
    [Table("DDORegistration")]
    public class DDORegistration
    {
        [Key]
        public int RegistrationID { get; set; }
        
        public string DDOCode { get; set; }
        
        public int DepartmentID { get; set; }

        public int DesignationID { get; set; }

        public int DistrictID { get; set; }

        public string OfficeAddress1 { get; set; }

        public string OfficeAddress2 { get; set; }

        public string TINNumber { get; set; }
        
        public string TANNumber { get; set; }

        public string EmailID { get; set; }

        public string ContactNumber { get; set; }

        public bool IsApproved { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }
    }
}
