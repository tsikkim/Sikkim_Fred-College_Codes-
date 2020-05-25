using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DDOController : ControllerBase
    {
        private readonly IDDORepository ddoRepository;
        private readonly ILogger<DDOController> logger;
        public DDOController(IDDORepository ddoRepository, ILogger<DDOController> logger)
        {
            this.ddoRepository = ddoRepository;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var ddoList = this.ddoRepository.GetAllDDOCodeBases();

                return new JsonResult(ddoList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while getting all DDOs.");
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [Route("search")]
        [HttpGet]
        public ActionResult Get([FromQuery] int deptId)
        {
            try
            {
                var ddoList =this.ddoRepository.GetDDOBaseByDeparmentId(deptId); ;

                return new JsonResult(ddoList);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error while getting DDO for deptId {0}.", deptId);
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [Route("details")]
        [HttpGet]
        public ActionResult<DDODetails> GetDDODetails([FromQuery] string ddoCode)
        {
            try
            {
                var ddoDetails = this.ddoRepository.GetDDODetailsByDDOCode(ddoCode);
                if (ddoDetails != null)
                    return ddoDetails;
                else
                    throw new NotFoundException($"DDO with code {ddoCode} does not exist.");
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex, $"Error while getting DDO Details for DDO Code {ddoCode}.");
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new JsonResult(new { Error = new { Message = ex.Message } });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while getting DDO Details for DDO Code {ddoCode}.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}
