using System.ComponentModel.DataAnnotations;

namespace SikkimGov.Platform.Models.ApiModels
{
    public class DDORegistrationModel
    {
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public string DDOCode { get; set; }
        [Required]
        public int DesignationId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public string OfficeAddress1 { get; set; }
        public string OfficeAddress2 { get; set; }
        public string TINNumber { get; set; }
        [Required]
        public string TANNumber { get; set; }
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string ContactNumber { get; set; }
    }
}
