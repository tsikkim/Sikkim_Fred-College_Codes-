using System;
using System.Collections.Generic;
using System.Text;

namespace SikkimGov.Platform.Common.Models
{
    public class EmailMessageModel
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string EmailBody { get; set; }
    }
}
