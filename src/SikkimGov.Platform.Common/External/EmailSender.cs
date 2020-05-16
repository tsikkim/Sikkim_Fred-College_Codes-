using System;
using System.Configuration;
using System.Net;
using MailKit.Net.Smtp;
using MimeKit;
using SikkimGov.Platform.Common.External.Contracts;
using SikkimGov.Platform.Common.Models;

namespace SikkimGov.Platform.Common.External
{
    public class EmailSender : IEmailSender
    {
        private string smtpHost;
        private int smtpPort;
        private string smtpUsername;
        private string smtpPassword;
        private string fromEmailId;

        public EmailSender()
        {
            this.smtpHost = ConfigurationManager.AppSettings["smtpHost"];
            this.smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
            this.smtpUsername = ConfigurationManager.AppSettings["smtpUsername"];
            this.smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];
            this.fromEmailId = ConfigurationManager.AppSettings["fromEmailId"];
        }

        public void SendEmail(EmailMessageModel mailMessage)
        {
            var message = BuildMessage(mailMessage);

            using (var smtp = GetSmtpClient())
            {
                smtp.Send(message);
            }
        }

        private MimeMessage BuildMessage(EmailMessageModel messageModel)
        {
            var mimeMessage = new MimeMessage();

            BodyBuilder builder = new BodyBuilder();
            builder.HtmlBody = messageModel.EmailBody;
            mimeMessage.Body = builder.ToMessageBody();

            mimeMessage.Subject = messageModel.Subject;
            MailboxAddress to = new MailboxAddress(messageModel.To);
            mimeMessage.To.Add(to);

            MailboxAddress from = new MailboxAddress(fromEmailId);
            mimeMessage.From.Add(from);

            return mimeMessage;
        }

        private SmtpClient GetSmtpClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var smtp = new SmtpClient();
            smtp.Connect(smtpHost, smtpPort, false);
            smtp.Authenticate(smtpUsername, smtpPassword);
            return smtp;
        }
    }
}
