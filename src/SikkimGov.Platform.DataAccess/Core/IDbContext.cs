using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Core
{
    public interface IDbContext
    {
        DbSet<Department> Departments { get; set; }
        
        DbSet<Designation> Designations { get; set; }
        
        DbSet<District> Districts { get; set; }

        DbSet<SBSPayment> SBSPayments { get; set; }

        DbSet<SBSReceipt> SBSReceipts { get; set; }

        DbSet<DDORegistration> DDORegistrations { get; set; }

        DbSet<RCORegistration> RCORegistrations { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<DDOInfo> DDOInfos { get; set; }

        DbSet<Feedback> Feedbacks { get; set; }

        void AddEntities<T>(IList<T> entities) where T : class;

        int SaveChanges();
    }
}
