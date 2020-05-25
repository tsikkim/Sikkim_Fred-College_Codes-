using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DDORegistrationController : ControllerBase
    {
        private readonly IDDORegistraionService registraionService;
        private readonly ILogger<DDORegistrationController> logger;
        public DDORegistrationController(IDDORegistraionService registraionService, ILogger<DDORegistrationController> logger)
        {
            this.registraionService = registraionService;
            this.logger = logger;
        }

        // POST: api/DDORegistration
        [HttpPost]
        public ActionResult Post([FromBody] DDORegistrationModel ddoRegistration)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                else
                {
                    this.registraionService.SaveRegistration(ddoRegistration);
                    return new JsonResult(new { Msg = "success" });
                }
            }
            catch (UserAlreadyExistsException ex)
            {
                this.logger.LogError(ex, "Error while creating DDO Registration.");
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult(new { Error = new { Message = ex.Message } });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while creating DDO Registration.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Get("all");
        }

        [HttpGet("{status}")]
        public ActionResult Get(string status)
        {
            try
            {
                var registrations = new List<DDORegistrationDetails>();
                switch (status.ToLower())
                {
                    case "all":
                        registrations = this.registraionService.GetAllRegistrations();
                        return new JsonResult(registrations);
                    case "approved":
                        registrations = this.registraionService.GetApprovedRegistrations();
                        return new JsonResult(registrations);
                    case "pending":
                        registrations = this.registraionService.GetPendingRegistrations();
                        return new JsonResult(registrations);
                    default:
                        this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return new JsonResult(new { Error = new { Message = "Invalid status filter provided." } });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while getting {0} DDO Registrations.", status);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [Route("approve")]
        [HttpPost]
        public ActionResult Approve([FromBody] RegistrationApprovalModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    this.registraionService.ApproveDDORegistration(model.RegId, model.ApprovedBy);
                    return new JsonResult(new { Msg = "success" });
                }
            }
            catch (NotFoundException ex)
            {
                this.logger.LogError(ex, "Error while approving DDO Registration with Id - {0}", model.RegId);
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new JsonResult(new { Error = new { Message = ex.Message } });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while approving DDO Registration with Id - {0}", model.RegId);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [HttpDelete("{regId}")]
        public ActionResult Delete(long regId)
        {
            try
            {
                this.registraionService.RejectDDORegistration(regId);
                return new JsonResult(new { Msg = "success" });
            }
            catch (NotFoundException ex)
            {
                this.logger.LogError(ex, "Error while deleting DDO Registration with Id - {0}", regId);
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new JsonResult(new { Error = new { Message = ex.Message } });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while deleting DDO Registration with Id - {0}", regId);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}
