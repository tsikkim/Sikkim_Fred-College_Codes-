using System.Collections.Generic;
using SikkimGov.Platform.Models.ApiModels;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IFeedbackService
    {
        FeedbackModel CreateFeedback(FeedbackModel feedback);

        List<Feedback> GetAllFeedbacks();

        bool DeleteFeedbackById(int id);
    }
}
