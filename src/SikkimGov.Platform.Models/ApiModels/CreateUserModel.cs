using System.ComponentModel.DataAnnotations;

namespace SikkimGov.Platform.Models.ApiModels
{
    public class CreateUserModel
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact number should be 10 digits.")]
        public string MobileNumber { get; set; }

        [MinLength(6, ErrorMessage = "Password must have minimum length of 6 characters.")]
        [MaxLength(20, ErrorMessage = "Password must have maximum length of 20 characters.")]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("Admin|SuperAdmin", ErrorMessage = "The field UserType must have value Admin or SuperAdmin.")]
        public string UserType { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="DepartmentId can not be zero.")]
        public int? DepartmentId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "DistrictId can not be zero.")]
        public int? DistrictId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "DesignationId can not be zero.")]
        public int? DesignationId { get; set; }
    }
}
