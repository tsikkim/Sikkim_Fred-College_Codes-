﻿using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SikkimGov.Platform.Business.Services.Contracts;
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
                        return BadRequest("Invalid username or password.");
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