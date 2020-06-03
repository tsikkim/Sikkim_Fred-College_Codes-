using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.Models.ApiModels;

namespace SikkimGov.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService feedbackService;
        private readonly ILogger<FeedbackController> logger;
        public FeedbackController(IFeedbackService feedbackService, ILogger<FeedbackController> logger)
        {
            this.feedbackService = feedbackService;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var result = this.feedbackService.GetAllFeedbacks();
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while getting all feedback.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [HttpPost()]
        public ActionResult Post([FromBody] FeedbackModel model)
        {
            try
            {
                var result = this.feedbackService.CreateFeedback(model);

                if (result.ID > 0)
                {
                    return new JsonResult(new { Msg = "success" });
                }
                else
                {
                    this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return new JsonResult(new { Error = new { Message = "Feedback could not be saved." } });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while getting creating feedback.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = this.feedbackService.DeleteFeedbackById(id);
                if (result)
                {
                    return new JsonResult(new { Msg = "success" });
                }
                else
                {
                    this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return new JsonResult(new { Error = new { Message = "Feedback could not be saved." } });
                }
            }
            catch(NotFoundException ex)
            {
                this.logger.LogError(ex, $"Error while getting deleting feedback with id : {0}.");
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new JsonResult(new { Error = new { Message = ex.Message } });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error while getting deleting feedback with id : {0}.");
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { Error = new { Message = "An unhandled error occured during request processing." } });
            }
        }
    }
}