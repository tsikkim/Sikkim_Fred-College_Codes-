using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SBSReceiptController : ControllerBase
    {
        private readonly ISBSReceiptRepository receiptRepository;
        private readonly ILogger<SBSReceiptController> logger;

        public SBSReceiptController(ISBSReceiptRepository paymentRepository, ILogger<SBSReceiptController> logger)
        {
            this.receiptRepository = paymentRepository;
            this.logger = logger;
        }

        [HttpGet("permonth/{startMonth}-{startYear}/{endMonth}-{endYear}")]
        public ActionResult GetPaymentsPerMonth(
            [Range(1, 12)] int startMonth,
            [Range(1, int.MaxValue, ErrorMessage = "StartYear must be greater than 0.")] int startYear,
            [Range(1, 12)] int endMonth,
            [Range(1, int.MaxValue, ErrorMessage = "StartYear must be greater than 0.")] int endYear)
        {
            try
            {
                if (startYear > endYear)
                {
                    return new JsonResult(new { Error = new { Message = "StartYear can not be greater than EndYear." } });
                }

                if (startYear == endYear && startMonth > endMonth)
                {
                    return new JsonResult(new { Error = new { Message = "StartMongh can not be greater than EndMonth." } });
                }
                var data = this.receiptRepository.GetAmountPerMonth(startMonth, startYear, endMonth, endYear);

                return new JsonResult(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while getting payments per month.");
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [HttpGet("permonth/{startYear}-{endYear}")]
        public ActionResult GetPaymentsPerMonthForYearRange(
            [Range(1, int.MaxValue, ErrorMessage = "StartYear must be greater than 0.")] int startYear,
            [Range(1, int.MaxValue, ErrorMessage = "StartYear must be greater than 0.")] int endYear)
        {
            try
            {
                if (startYear > endYear)
                {
                    return new JsonResult(new { Error = new { Message = "StartYear can not be greater than EndYear." } });
                }
                var data = this.receiptRepository.GetAmountPerMonthForYearRange(startYear, endYear);

                return new JsonResult(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while getting payments per month.");
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}
