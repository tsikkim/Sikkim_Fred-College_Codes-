
using System.ComponentModel.DataAnnotations;

namespace SikkimGov.Platform.Models.ApiModels
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
