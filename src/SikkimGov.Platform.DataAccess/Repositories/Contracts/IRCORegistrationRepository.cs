using System;
using System.Collections.Generic;
using System.Text;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IRCORegistrationRepository
    {
        RCORegistration SaveRCORegistration(RCORegistration rcoRegistration);
    }
}
