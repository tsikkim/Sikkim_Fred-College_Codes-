namespace SikkimGov.Platform.Common.Models
{
    public class LoginDetailsEmailModel : EmailModel
    {
        public string ReceiverName { get; set; }

        public string EmailId { get; set; }

        public string Password { get; set; }
    }
}
