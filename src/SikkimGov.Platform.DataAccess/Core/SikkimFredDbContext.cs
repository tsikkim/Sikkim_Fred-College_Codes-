using Microsoft.EntityFrameworkCore;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Core
{
    public class SikkimFredDbContext : DbContext
    {
        public SikkimFredDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<District> Districts { get; set; }
    }
}
