using Microsoft.AspNetCore.Mvc;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Models.DomainModels;

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DDORegistrationController : ControllerBase
    {
        private readonly IDDORegistraionService registraionService;

        public DDORegistrationController(IDDORegistraionService registraionService)
        {
            this.registraionService = registraionService;
        }

        // POST: api/DDORegistration
        [HttpPost]
        public void Post([FromBody] DDORegistration ddoRegistration)
        {
            this.registraionService.SaveRegistration(ddoRegistration);
        }
    }
}
