using System.IO;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using SikkimGov.Platform.Common.External.Contracts;
using SikkimGov.Platform.Common.Models;

namespace SikkimGov.Platform.Common.External
{
    public class EmailService : IEmailService
    {
        private readonly IHostingEnvironment environment;
        private readonly IEmailSender emailSender;

        public EmailService(IHostingEnvironment environment, IEmailSender emailSender)
        {
            this.environment = environment;
            this.emailSender = emailSender;
        }

        public void SendLoginDetails(LoginDetailsEmailModel model)
        {
            var templateFilePath = Directory.GetCurrentDirectory()
                            + Path.DirectorySeparatorChar.ToString()
                            + "Templates"
                            + Path.DirectorySeparatorChar.ToString()
                            + "EmailTemplates"
                            + Path.DirectorySeparatorChar.ToString()
                            + "LoginDetailsEmail.html";

            var builder = new BodyBuilder();

            using (StreamReader SourceReader = File.OpenText(templateFilePath))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string messageBody = string.Format(builder.HtmlBody, model.UserName, model.EmailId, model.Password);

            EmailMessageModel messageModel = new EmailMessageModel();
            messageModel.EmailBody = messageBody;
            messageModel.Subject = model.Subject;
            messageModel.To = model.ReceiverEmail;

            this.emailSender.SendEmail(messageModel);
        }
    }
}
