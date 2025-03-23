using AutoMapper;
using MindSpace.Application.DTOs;
using MindSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Profiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<Feedback, FeedbackDTO>();
        }
    }
}
