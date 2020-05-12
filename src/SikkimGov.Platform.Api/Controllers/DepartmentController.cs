using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        // GET: api/Department
        [HttpGet]
        public List<Department> Get()
        {
            var department = this.departmentRepository.GetAllDepartments();
            return this.departmentRepository.GetAllDepartments();
        }

        // GET: api/Department/5
        [HttpGet("{id}", Name = "Get")]
        public Department Get(int id)
        {
            return this.departmentRepository.GetDepartmentById(id);
        }
    }
}
