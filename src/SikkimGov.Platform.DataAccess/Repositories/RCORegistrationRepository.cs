using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class RCORegistrationRepository : BaseRepository, IRCORegistrationRepository
    {
        private const string RCO_REG_SAVE_COMMAND = "P_RCO_Registration_INS";
        private const string RCO_REG_DEL_COMMAND = "P_DEL_RCO_REGISTRATION";
        private const string RCO_REG_GET_BY_ID_COMMAND = "P_RCO_REG_READ_BY_ID";
        private const string RDO_REG_UPDATE_STATUS_COMMAND = "P_RCO_REG_UPDATE_STATUS";
        private const string RCO_REG_DETAILS_READ_BY_STATUS_COMMAND = "P_RCO_REG_DETAILS_READ_BY_STATUS";

        private readonly IDbContext dbContext;

        public RCORegistrationRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public RCORegistration CreateRCORegistration(RCORegistration rcoRegistration)
        {
            this.dbContext.RCORegistrations.Add(rcoRegistration);
            this.dbContext.SaveChanges();

            return rcoRegistration;
        }

        public bool DeleteRCORegistration(RCORegistration registration)
        {
            this.dbContext.RCORegistrations.Remove(registration);
            var result = this.dbContext.SaveChanges();

            return result != 0;
        }

        public RCORegistration GetRCORegistrationById(long rcoRegistrationId)
        {
            return this.dbContext.RCORegistrations.FirstOrDefault(registraion => registraion.RegistrationID == rcoRegistrationId);
        }

        public List<Models.DomainModels.RCORegistrationDetails> GetRCORegistrationsByStatus(bool? status)
        {
            return new List<Models.DomainModels.RCORegistrationDetails>();
        }

        public RCORegistration UpdateRegistration(RCORegistration rcoRegistration)
        {
            this.dbContext.RCORegistrations.Update(rcoRegistration);
            this.dbContext.SaveChanges();

            return rcoRegistration;
        }
    }
}