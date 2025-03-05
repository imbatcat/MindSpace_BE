using MindSpace.Application.Interfaces.Specifications;
using MindSpace.Domain.Entities;

namespace MindSpace.Application.Interfaces.Services
{
    public interface IFeedbackService
    {
        public Task<List<Feedback>> GetAllFeedBackAsync(ISpecification<Feedback> spec);
        public Task<bool> CreateFeedBack(Feedback feedback, int studentId, int psychologistId);
        public Task<bool> RemoveFeedBack(Feedback feedback);
        public Task<List<Feedback>> GetTopFeedBackAsync(ISpecification<Feedback> spec, int count);
    }
}
