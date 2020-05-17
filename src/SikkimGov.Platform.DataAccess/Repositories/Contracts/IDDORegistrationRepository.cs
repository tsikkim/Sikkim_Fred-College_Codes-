using System.Collections.Generic;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IDDORegistrationRepository
    {
        DDORegistration CreateDDORegistration(DDORegistration ddoRegistration);

        List<DDORegistrationDetails> GetDDORegistrationsByStatus(bool? status);

        bool DeleteDDORegistration(long ddoRegistrationId);

        DDORegistration GetDDORegistrationById(long ddoRegistrationId);

        bool UpdateDDORegistrationStatus(long ddoRegistrationId, bool status, int updatedBy);
    }
}
