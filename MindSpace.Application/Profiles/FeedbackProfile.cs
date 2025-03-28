using AutoMapper;
using MindSpace.Application.DTOs;
using MindSpace.Application.Features.Feedbacks.CreateFeedbackForPsychologist;
using MindSpace.Domain.Entities;

namespace MindSpace.Application.Profiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<CreateFeedbackForPsychologistCommand, Feedback>();
            CreateMap<Feedback, FeedbackDTO>();
        }
    }
}
