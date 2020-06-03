using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Common.Exceptions;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.Business.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository feedbackRepository;
        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }
        public FeedbackModel CreateFeedback(FeedbackModel feedbackModel)
        {
            var newFeedBack = new Feedback();
            newFeedBack.Name = feedbackModel.Name;
            newFeedBack.EmailID = feedbackModel.EmailID;
            newFeedBack.Address = feedbackModel.Address;
            newFeedBack.ContactNumber = feedbackModel.ContactNumber;
            newFeedBack.Comments = feedbackModel.Comments;
            newFeedBack.CreatedDate = DateTime.Now;
            var feedback = this.feedbackRepository.CreateFeedback(newFeedBack);

            feedbackModel.ID = feedback.ID;

            return feedbackModel;
        }

        public List<Feedback> GetAllFeedbacks()
        {
            return this.feedbackRepository.GetAllFeedbacks();
        }

        public bool DeleteFeedbackById(int id)
        {
            var feedback = this.feedbackRepository.GetFeedbackById(id);

            if(feedback == null)
            {
                throw new NotFoundException($"Feedback with id {id} does not exist.");
            }

            return this.feedbackRepository.DeleteFeedback(feedback);
        }
    }
}
