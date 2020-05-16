using System.ComponentModel.DataAnnotations;

namespace SikkimGov.Platform.Models.ApiModels
{
    public class RegistrationApprovalModel
    {
        [Required]
        public long RegId { get; set; }

        [Required]
        public int ApprovedBy { get; set; }
    }
}
