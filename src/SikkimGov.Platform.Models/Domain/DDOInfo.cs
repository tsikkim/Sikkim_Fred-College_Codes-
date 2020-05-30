using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SikkimGov.Platform.Models.Domain
{
    [Table("DDOInfo")]
    public class DDOInfo
    {
        [Key]
        public int ID { get; set; }

        public int DepartmentID { get; set; }

        public int DistrictID { get; set; }

        public int DesignationID { get; set; }

        public string DDOCode { get; set; }
        
        public string Name { get; set; }

        public string EmailID { get; set; }

        public string ContactNumber { get; set; }

        public bool IsApproved { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ApprovedBy { get; set; }

        public DateTime ApprovedDate { get; set; }
    }
}
