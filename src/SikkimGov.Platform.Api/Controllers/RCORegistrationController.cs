using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.Models.ApiModels;

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RCORegistrationController : ControllerBase
    {
        private readonly IRCORegistrationService registraionService;

        public RCORegistrationController(IRCORegistrationService service)
        {
            this.registraionService = service;
        }

        // POST: api/RCORegistration
        [HttpPost]
        public ActionResult Post([FromBody] RCORegistrationModel rcoRegistration)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                else
                {
                    this.registraionService.SaveRegistration(rcoRegistration);
                    this.Response.StatusCode = (int)HttpStatusCode.Created;
                    return new EmptyResult();
                }
            }
            catch(UserAlreadyExistsException ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult(new { Error = new { Message = ex.Message } });
            }
            catch(Exception ex)
            {
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
                    this.registraionService.ApproveRCORegistration(model.RegId, model.ApprovedBy);
                    return new EmptyResult();
                }
            }
            catch (NotFoundException ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new JsonResult(new { Error = new { Message = ex.Message } });
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [HttpDelete("{regId}")]
        public ActionResult Delete(long regId)
        {
            try
            {
                this.registraionService.RejectRCORegistration(regId);
                return new EmptyResult();
            }
            catch (NotFoundException ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new JsonResult(new { Error = new { Message = ex.Message } });
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}
