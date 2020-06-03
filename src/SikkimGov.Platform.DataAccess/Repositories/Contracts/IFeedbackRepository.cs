using System.Collections.Generic;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories.Contracts
{
    public interface IFeedbackRepository
    {
        Feedback CreateFeedback(Feedback feedback);

        List<Feedback> GetAllFeedbacks();

        bool DeleteFeedbackById(int id);

        bool DeleteFeedback(Feedback feedback);

        Feedback GetFeedbackById(int id);
    }
}
