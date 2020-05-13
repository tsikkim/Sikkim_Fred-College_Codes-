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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                var createdDDO = this.registraionService.SaveRegistration(ddoRegistration);
                return CreatedAtAction(nameof(Get), new { id = createdDDO.Id }, createdDDO);
            }
        }

        [HttpGet("{id}")]
        public DDORegistration Get(long id)
        {
            return new DDORegistration();
        }
    }
}
