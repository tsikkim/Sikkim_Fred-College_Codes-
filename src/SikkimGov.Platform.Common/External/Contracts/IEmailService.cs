using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace SikkimGov.Platform.Common.External.Contracts
{
    public interface IEmailService
    {
        void SendEmail(MailMessage mailMessage);
    }
}
