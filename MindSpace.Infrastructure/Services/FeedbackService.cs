using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Specifications;
using MindSpace.Domain.Entities;

namespace MindSpace.Infrastructure.Services
{
    public class FeedbackService : IFeedbackService
    {
        public Task<bool> CreateFeedBack(Feedback feedback, int studentId, int psychologistId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Feedback>> GetAllFeedBackAsync(ISpecification<Feedback> spec)
        {
            throw new NotImplementedException();
        }

        public Task<List<Feedback>> GetTopFeedBackAsync(ISpecification<Feedback> spec, int count)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFeedBack(Feedback feedback)
        {
            throw new NotImplementedException();
        }
    }
}
