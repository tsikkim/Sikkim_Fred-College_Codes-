using System.Collections.Generic;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IDDORegistrationRepository
    {
        DDORegistration SaveDDORegistration(DDORegistration ddoRegistration);

        List<DDORegistration> GetDDORegistrationsByStatus(bool status);
    }
}
