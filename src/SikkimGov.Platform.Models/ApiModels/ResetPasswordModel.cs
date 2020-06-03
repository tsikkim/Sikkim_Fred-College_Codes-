using System.ComponentModel.DataAnnotations;

namespace SikkimGov.Platform.Models.ApiModels
{
    public class ResetPasswordModel
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must have minimum length of 6 characters.")]
        [MaxLength(20, ErrorMessage = "Password must have maximum length of 20 characters.")]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
