using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.Models.ApiModels;

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthenticationService authenticationService;

        public UserController(IUserService userService, IAuthenticationService authenticationService)
        {
            this.userService = userService;
            this.authenticationService = authenticationService;
        }

        [Route("exists/{userName}")]
        [HttpGet]
        public bool IsUserExists(string userName)
        {
            return this.userService.IsUserExists(userName);
        }

        [Route("recoverpassword")]
        [HttpPost]
        public ActionResult RecoverPassword(ForgetPasswordModel model)
        {
            try
            {
                if(string.IsNullOrEmpty(model.UserName))
                {
                    return BadRequest(new { Error = new { Message = "Username can not be empty." } });
                }

                this.userService.SendLoginDetails(model.UserName);
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

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody]LoginModel login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    var authenticationResult = this.authenticationService.AuthenticateUser(login);
                    if (authenticationResult.IsAuthenticated)
                    {
                        return Ok(authenticationResult);
                    }
                    else
                    {
                        this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return new JsonResult(new { Error = new { Message = "Invalid username or password." } });
                    }
                }
            }
            catch(Exception ex)
            {
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}
