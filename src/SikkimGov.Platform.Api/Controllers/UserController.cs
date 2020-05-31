using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.Domain;
using SikkimGov.Platform.Models.DomainModels;

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

        [HttpPost]
        public ActionResult Post([FromBody]CreateUserModel model)
        {
            try
            {
                if(this.userService.IsUserExists(model.EmailId))
                {
                    throw new UserAlreadyExistsException($"User with emailid {model.EmailId} already exists.");
                }

                var newUser = new Models.Domain.User();
                newUser.FirstName = model.FirstName;
                newUser.LastName = model.LastName;
                newUser.EmailID = model.EmailId;
                newUser.DepartmentID = model.DepartmentId.Value;
                newUser.DesingationID = model.DesignationId.Value;
                newUser.DistrictID = model.DistrictId.Value;
                newUser.MobileNumber = model.MobileNumber;

                newUser.UserType = (UserType)Enum.Parse(typeof(UserType), model.UserType, true);
                newUser = this.userService.CreateUser(newUser, model.Password);

                var userDetails = new UserDetails();
                userDetails.Id = newUser.UserID;
                userDetails.EmailId = newUser.EmailID;
                userDetails.FirstName = newUser.FirstName;
                userDetails.LastName = newUser.LastName;
                userDetails.IsAdmin = newUser.UserType == UserType.Admin;
                userDetails.IsSuperAdmin = newUser.UserType == UserType.SuperAdmin;
                userDetails.UserType = model.UserType;

                return new JsonResult(userDetails);
            }
            catch(UserAlreadyExistsException ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult(new
                {
                    Error = new { Message = ex.Message }
                });
            }
            catch (NotFoundException ex)
            {
                this.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return new JsonResult(new
                {
                    Error = new { Message = ex.Message }
                });
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
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
                if (string.IsNullOrEmpty(model.UserName))
                {
                    return BadRequest(new { Error = new { Message = "Username can not be empty." } });
                }

                this.userService.SendLoginDetails(model.UserName);
                return new JsonResult(new { Msg = "success" });
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

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody] LoginModel login)
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
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [Route("ddo")]
        [HttpGet]
        public ActionResult GetDDOUsers()
        {
            try
            {
                var users = this.userService.GetDDOUserDetails();
                return new JsonResult(users);
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [Route("rco")]
        [HttpGet]
        public ActionResult GetRCOUsers()
        {
            try
            {
                var users = this.userService.GetRCOUserDetails();
                return new JsonResult(users);
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [Route("admin")]
        [HttpGet]
        public ActionResult GetAdminUsers()
        {
            try
            {
                var users = this.userService.GetAdminUserDetails();
                return new JsonResult(users);
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}
