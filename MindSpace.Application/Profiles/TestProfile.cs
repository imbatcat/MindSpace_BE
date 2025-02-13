using AutoMapper;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Domain.Entities.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<CreateTestWithoutQuestionsDTO, Test>()
                .ForMember(d => d.Title, a => a.MapFrom(t => t.Title))
                .ForMember(d => d.TestCode, a => a.MapFrom(t => t.TestCode))
                .ForMember(d => d.TargetUser, a => a.MapFrom(t => t.TargetUser))
                .ForMember(d => d.Description, a => a.MapFrom(t => t.Description))
                .ForMember(d => d.Price, a => a.MapFrom(t => t.Price))
                .ForMember(d => d.AuthorId, a => a.MapFrom(t => t.AuthorId))
                .ForMember(d => d.TestCategoryId, a => a.MapFrom(t => t.TestCategoryId))
                .ForMember(d => d.SpecializationId, a => a.MapFrom(t => t.SpecializationId));

            CreateProjection<Test, TestResponseDTO>()
                .ForMember(d => d.Title, a => a.MapFrom(t => t.Title != null ? t.Title : null))
                .ForMember(d => d.TestCode, a => a.MapFrom(t => t.TestCode != null ? t.TestCode : null))
                .ForMember(d => d.TargetUser, a => a.MapFrom(t => t.TargetUser))
                .ForMember(d => d.TestCategoryId, a => a.MapFrom(t => t.TestCategoryId))
                .ForMember(d => d.SpecializationId, a => a.MapFrom(t => t.SpecializationId))
                .ForMember(d => d.Questions, a => a.MapFrom(t => t.TestQuestions.Select(tq => tq.Question))) // get questions from TestQuestions
                .ForMember(d => d.PsychologyTestOptions, a => a.MapFrom(t => t.PsychologyTestOptions.Select(pto => pto))) // get psychology test options from PsychologyTestOptions
                .ForMember(d => d.TestScoreRanks, a => a.MapFrom(t => t.TestScoreRanks.Select(tsr => tsr))); // get test score ranks from TestScoreRanks

            CreateMap<Test, TestResponseDTO>()
                .ForMember(d => d.Title, a => a.MapFrom(t => t.Title))
                .ForMember(d => d.TestCode, a => a.MapFrom(t => t.TestCode))
                .ForMember(d => d.TargetUser, a => a.MapFrom(t => t.TargetUser))
                .ForMember(d => d.Description, a => a.MapFrom(t => t.Description))
                .ForMember(d => d.Price, a => a.MapFrom(t => t.Price))
                .ForMember(d => d.AuthorId, a => a.MapFrom(t => t.AuthorId))
                .ForMember(d => d.TestCategoryId, a => a.MapFrom(t => t.TestCategoryId))
                .ForMember(d => d.SpecializationId, a => a.MapFrom(t => t.SpecializationId));

        }
    }
}
