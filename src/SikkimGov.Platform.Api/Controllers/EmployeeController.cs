using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SikkimGov.Platform.Common.External.Contracts;

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeApiService employeeApiService;
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(IEmployeeApiService employeeApiService, ILogger<EmployeeController> logger)
        {
            this.employeeApiService = employeeApiService;
            this.logger = logger;
        }

        [HttpGet("salary")]
        public ActionResult SalaryDetails([FromQuery]string empCode,
            [FromQuery][Range(1, 12)] int month,
            [FromQuery][Range(1, int.MaxValue, ErrorMessage = "Year must be greater than 0.")] int year)
        {
            try
            {
                var salary = employeeApiService.GetSalaryDetails(empCode, month, year);
                return new JsonResult(salary);
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex, $"Error while getting salary for empCode : {empCode}, month: {month}, year: {year}.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [HttpGet("details")]
        public ActionResult Details([FromQuery]string empCode,
            [FromQuery]
            [Range(1, int.MaxValue, ErrorMessage = "OfficeId must be greater than 0.")] int officeid)
        {
            try
            {
                var employeeDetails = employeeApiService.GetEmployeeDetails(empCode, officeid);
                return new JsonResult(employeeDetails);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error while getting details for empCode : {empCode}, officeid: {officeid}.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}