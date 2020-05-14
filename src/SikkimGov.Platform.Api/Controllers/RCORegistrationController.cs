using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SikkimGov.Platform.Business.Services.Contracts;
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
        public ActionResult Post([FromBody] RCORegistrationModel ddoRegistration)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                else
                {
                    this.registraionService.SaveRegistration(ddoRegistration);
                    this.Response.StatusCode = (int)HttpStatusCode.Created;
                    return new EmptyResult();
                }
            }
            catch(Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}
