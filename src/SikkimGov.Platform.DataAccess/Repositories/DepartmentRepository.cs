using System.Collections.Generic;
using System.Linq;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class DepartmentRepository : BaseRepository, IDepartmentRepository
    {
        private readonly IDbContext dbContext;

        public DepartmentRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private const string DEPT_READ_COMMAND = "P_READ_DEMAND";
        public IEnumerable<Department> GetAllDepartments()
        {
            return this.dbContext.Departments;
        }

        public Department GetDepartmentById(long departmentId)
        {

            return this.dbContext.Departments
                        .FirstOrDefault(department => department.Id == departmentId);
        }
    }
}