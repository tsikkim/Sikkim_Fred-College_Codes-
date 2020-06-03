using System.Collections.Generic;
using System.Linq;
using SikkimGov.Platform.DataAccess.Core;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.DataAccess.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IDbContext dbContext;

        public FeedbackRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Feedback CreateFeedback(Feedback feedback)
        {
            this.dbContext.Feedbacks.Add(feedback);
            this.dbContext.SaveChanges();
            return feedback;
        }

        public bool DeleteFeedbackById(int id)
        {
            var feedback = this.dbContext.Feedbacks.FirstOrDefault(fback => fback.ID == id);
            if(feedback != null)
            {
                this.dbContext.Feedbacks.Remove(feedback);
                var result = this.dbContext.SaveChanges();

                return result != 0;
            }

            return false;
        }

        public bool DeleteFeedback(Feedback feedback)
        {
            this.dbContext.Feedbacks.Remove(feedback);
            var result = this.dbContext.SaveChanges();

            return result > 0;
        }

        public Feedback GetFeedbackById(int id)
        {
            return this.dbContext.Feedbacks.FirstOrDefault(fback => fback.ID == id);
        }

        public List<Feedback> GetAllFeedbacks()
        {
            return this.dbContext.Feedbacks.ToList();
        }
    }
}
