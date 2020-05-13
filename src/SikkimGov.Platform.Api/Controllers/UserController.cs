using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SikkimGov.Platform.Business.Services.Contracts;

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("exists/{userName}")]
        [HttpGet]
        public bool IsUserExists(string userName)
        {
            return this.userService.IsUserExists(userName);
        }
    }
}
