using System.Collections.Generic;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IDDORegistrationRepository
    {
        DDORegistration CreateDDORegistration(DDORegistration ddoRegistration);

        List<Models.DomainModels.DDORegistrationDetails> GetDDORegistrationsByStatus(bool? status);

        bool DeleteDDORegistration(DDORegistration ddoRegistration);

        DDORegistration GetDDORegistrationById(long ddoRegistrationId);

        bool UpdateDDORegistrationStatus(long ddoRegistrationId, bool status, int updatedBy);

        DDORegistration UpdateRegistration(DDORegistration ddoRegistration);
    }
}
