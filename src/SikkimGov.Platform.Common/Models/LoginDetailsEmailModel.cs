namespace SikkimGov.Platform.Common.Models
{
    public class LoginDetailsEmailModel : EmailModel
    {
        public string UserName { get; set; }

        public string EmailId { get; set; }

        public string Password { get; set; }
    }
}
