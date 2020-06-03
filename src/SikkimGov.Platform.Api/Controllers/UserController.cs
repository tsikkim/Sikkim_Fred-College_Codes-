using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, IAuthenticationService authenticationService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.authenticationService = authenticationService;
            this.logger = logger;
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
                newUser.IsActive = true;

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
                logger.LogWarning(ex.Message);
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
                logger.LogError(ex, "Error while creating user.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [Route("exists/{emailId}")]
        [HttpGet]
        public ActionResult IsUserExists(string emailId)
        {
            try
            {
                var result = this.userService.IsUserExists(emailId);
                return new ContentResult { Content = result.ToString() };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while checkig if user existing with emailid - {emailId}.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [Route("recoverpassword")]
        [HttpPost]
        public ActionResult RecoverPassword(ForgetPasswordModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.EmailId))
                {
                    return BadRequest(new { Error = new { Message = "EmailId can not be empty." } });
                }

                this.userService.SendLoginDetails(model.EmailId);
                return new JsonResult(new { Msg = "success" });
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex, $"Error while recovering password for emailid : {model.EmailId}.");
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new JsonResult(new { Error = new { Message = ex.Message } });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while recovering password for emailid : {model.EmailId}.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [Route("resetpassword/{id}")]
        [HttpPost]
        public ActionResult ResetPassword(int id, [FromBody]ResetPasswordModel model)
        {
            try
            {
                if(model.CurrentPassword == model.NewPassword)
                {
                    return BadRequest(new { Error = new { Message = "Current password can not be used as new password." } });
                }

                if (model.NewPassword != model.ConfirmPassword)
                {
                    return BadRequest(new { Error = new { Message = "New password and confirm password must be the same." } });
                }

                var result = this.userService.ResetPassword(id, model.CurrentPassword, model.NewPassword);

                if(result)
                {
                    return new JsonResult(new { Msg = "success" });
                }
                else
                {
                    return BadRequest(new { Error = new { Message = "Current password is not valid." } });
                }
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex, $"Error while resetting password for userid : {id}.");
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new JsonResult(new { Error = new { Message = ex.Message } });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while resetting password for emailid : {id}.");
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
                        logger.LogWarning("User could not be authenticated with provided credentials.");
                        this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return new JsonResult(new { Error = new { Message = "Invalid emailid or password." } });
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while login for emailid : {login.EmailId}.");
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
                logger.LogError(ex, $"Error while getting DDO Users.");
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
                logger.LogError(ex, $"Error while getting RCO Users.");
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
                logger.LogError(ex, $"Error while getting Admin Users.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [HttpDelete("{userId}")]
        public ActionResult Delete(int userId)
        {
            try
            {
                this.userService.DeleteUserById(userId);
                return new JsonResult(new { Msg = "success" });
            }
            catch(NotFoundException ex)
            {
                logger.LogError(ex, $"Error while deleting user with id : {userId}.");
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new JsonResult(new { Error = new { Message = ex.Message } });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while deleting user with id : {userId}.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}