using System.Collections.Generic;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface d
    {
        Feedback CreateFeedback(Feedback feedback);
        bool DeleteFeedbackById(int feedbackId);
        List<Feedback> GetAllFeedbacks();
    }
}