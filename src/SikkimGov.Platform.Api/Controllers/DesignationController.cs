using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationRepository designationRepository;
        private readonly ILogger<DesignationController> logger;

        public DesignationController(IDesignationRepository designationRepository, ILogger<DesignationController> logger)
        {
            this.designationRepository = designationRepository;
            this.logger = logger;
        }

        // GET: api/<Designation>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var designations = this.designationRepository.GetAllDesignations();

                return new JsonResult(designations);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while getting all designations.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}
