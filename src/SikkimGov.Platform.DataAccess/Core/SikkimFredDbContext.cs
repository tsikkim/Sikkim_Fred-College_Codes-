using System.Collections.Generic;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Core
{
    public class SikkimFredDbContext : DbContext, IDbContext
    {
        public SikkimFredDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(c => c.UserType)
                .HasConversion<byte>();

            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Designation> Designations { get; set; }

        public virtual DbSet<District> Districts { get; set; }

        public virtual DbSet<SBSPayment> SBSPayments { get; set; }
        
        public virtual DbSet<SBSReceipt> SBSReceipts { get; set; }

        public virtual DbSet<DDORegistration> DDORegistrations { get; set; }

        public virtual DbSet<RCORegistration> RCORegistrations { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<DDOInfo> DDOInfos { get; set; }

        public virtual DbSet<Feedback> Feedbacks { get; set; }

        public void AddEntities<T>(IList<T> entities) where T: class
        {
            this.BulkInsert<T>(entities);
        }
    }
}
