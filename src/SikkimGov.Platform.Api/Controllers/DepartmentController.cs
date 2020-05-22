using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly ILogger<DepartmentController> logger;
        public DepartmentController(IDepartmentRepository departmentRepository, ILogger<DepartmentController> logger)
        {
            this.departmentRepository = departmentRepository;
            this.logger = logger;
        }

        // GET: api/Department
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                logger.LogInformation("Starting get method.");
                var departments = this.departmentRepository.GetAllDepartments();

                return new JsonResult(departments);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while getting all departments.");
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        // GET: api/Department/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            try
            {
                var department = this.departmentRepository.GetDepartmentById(id);

                if (department != null)
                    return new JsonResult(department);
                else
                {
                    this.Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
                    return new JsonResult(new { Error = new { Message = $"Department with ID {id} does not exist." } });
                }
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}
