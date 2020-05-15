using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using SikkimGov.Platform.Common.External.Contracts;

namespace SikkimGov.Platform.Common.External
{
    public class EmailService : IEmailService
    {
        private string smtpHost;
        private int smtpPort;
        private string smtpUsername;
        private string smtpPassword;

        public EmailService()
        {
            this.smtpHost = ConfigurationManager.AppSettings["smtpHost"];
            this.smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
            this.smtpUsername = ConfigurationManager.AppSettings["smtpUsername"];
            this.smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];
        }

        public void SendEmail(MailMessage mailMessage)
        {
            using (var smtp = GetSmtpClient())
            {
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("FromMailAddress", "password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mailMessage);
            }
        }

        private SmtpClient GetSmtpClient()
        {
            var smtp = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("FromMailAddress", "password"),
                DeliveryMethod = SmtpDeliveryMethod.Network,

            };

            return smtp;
        }
    }
}
