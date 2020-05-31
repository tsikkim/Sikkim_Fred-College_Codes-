using System;
using System.Collections.Generic;
using System.Text;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IDistrictRepository
    {
        List<District> GetAllDistricts();
    }
}
