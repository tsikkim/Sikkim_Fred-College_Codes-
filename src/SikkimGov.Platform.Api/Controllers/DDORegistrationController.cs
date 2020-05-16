using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class DDORegistrationController : ControllerBase
    {
        private readonly IDDORegistraionService registraionService;

        public DDORegistrationController(IDDORegistraionService registraionService)
        {
            this.registraionService = registraionService;
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
                    return new EmptyResult();
                }
            }
            catch (UserAlreadyExistsException ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult(new { Error = new { Message = ex.Message } });
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [HttpGet("{id}")]
        public DDORegistration Get(long id)
        {
            return new DDORegistration();
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
                this.registraionService.RejectDDORegistration(regId);
                return new EmptyResult();
            }
            catch(NotFoundException ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new JsonResult(new { Error = new { Message = ex.Message } });
            }
            catch(Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}
