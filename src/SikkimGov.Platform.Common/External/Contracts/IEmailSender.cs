using System;
using System.Collections.Generic;
using System.Text;
using SikkimGov.Platform.Common.Models;

namespace SikkimGov.Platform.Common.External.Contracts
{
    public interface IEmailSender
    {
        void SendEmail(EmailMessageModel mailMessage);
        
    }
}
