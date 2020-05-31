using System.ComponentModel.DataAnnotations;

namespace SikkimGov.Platform.Models.ApiModels
{
    public class RCORegistrationModel
    {
        public int Id { get; set; }

        [Required]
        public string AdminName { get; set; }
       
        [Required]
        public string RegistrationType { get; set; }
        
        [Required]
        public int DepartmentId { get; set; }
        
        [Required]
        public string Designation { get; set; }
        
        [Required]
        public string District { get; set; }
        
        [Required]
        public string OfficeAddress1 { get; set; }
        
        public string OfficeAddress2 { get; set; }
        public string TINNumber { get; set; }
        
        [Required]
        public string TANNumber { get; set; }
        
        [Required]
        [EmailAddress(ErrorMessage = "EmailId is not valid.")]
        public string EmailId { get; set; }
        
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact number should be 10 digits.")]
        public string ContactNumber { get; set; }
    }
}
