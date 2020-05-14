using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SikkimGov.Platform.Business.Services.Contracts;
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
                    this.Response.StatusCode = (int)HttpStatusCode.Created;
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.Created;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [HttpGet("{id}")]
        public DDORegistration Get(long id)
        {
            return new DDORegistration();
        }
    }
}
