using System.ComponentModel.DataAnnotations;

namespace SikkimGov.Platform.Models.ApiModels
{
    public class FeedbackModel
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress(ErrorMessage = "EmailId is not valid.")]
        public string EmailID { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact number should be 10 digits.")]
        public string ContactNumber { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Comments { get; set; }
    }
}