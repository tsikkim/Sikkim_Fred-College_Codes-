using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private IDistrictRepository districtRepository;
        private ILogger<DistrictController> logger;

        public DistrictController(IDistrictRepository districtRepository, ILogger<DistrictController> logger)
        {
            this.districtRepository = districtRepository;
            this.logger = logger;
        }

        // GET: api/<DistrictController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var districts = this.districtRepository.GetAllDistricts();

                return new JsonResult(districts);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while getting all districts.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}