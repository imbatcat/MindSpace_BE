using AutoMapper;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            // Projections
            CreateProjection<Test, TestResponseDTO>()
                .ForMember(d => d.Title, a => a.MapFrom(t => t.Title != null ? t.Title : null))
                .ForMember(d => d.TestCode, a => a.MapFrom(t => t.TestCode != null ? t.TestCode : null))
                .ForMember(d => d.TargetUser, a => a.MapFrom(t => t.TargetUser))
                .ForMember(d => d.Description, a => a.MapFrom(t => t.Description))
                .ForMember(d => d.Price, a => a.MapFrom(t => t.Price))
                .ForMember(d => d.Author, a => a.MapFrom(t => t.Author))
                .ForMember(d => d.TestCategory, a => a.MapFrom(t => t.TestCategory))
                .ForMember(d => d.Specialization, a => a.MapFrom(t => t.Specialization))
                .ForMember(d => d.TestStatus, a => a.MapFrom(t => t.TestStatus))
                .ForMember(d => d.Questions, a => a.MapFrom(t => t.TestQuestions.Select(tq => tq.Question))) // get questions from TestQuestions
                .ForMember(d => d.PsychologyTestOptions, a => a.MapFrom(t => t.PsychologyTestOptions.Select(pto => pto))) // get psychology test options from PsychologyTestOptions
                .ForMember(d => d.TestScoreRanks, a => a.MapFrom(t => t.TestScoreRanks.Select(tsr => tsr))); // get test score ranks from TestScoreRanks

            CreateProjection<Test, TestOverviewResponseDTO>()
                .ForMember(d => d.Title, a => a.MapFrom(t => t.Title))
                .ForMember(d => d.TestCode, a => a.MapFrom(t => t.TestCode))
                .ForMember(d => d.TargetUser, a => a.MapFrom(t => t.TargetUser))
                .ForMember(d => d.Description, a => a.MapFrom(t => t.Description))
                .ForMember(d => d.Price, a => a.MapFrom(t => t.Price))
                .ForMember(d => d.Author, a => a.MapFrom(t => t.Author))
                .ForMember(d => d.TestCategory, a => a.MapFrom(t => t.TestCategory))
                .ForMember(d => d.Specialization, a => a.MapFrom(t => t.Specialization))
                .ForMember(d => d.TestStatus, a => a.MapFrom(t => t.TestStatus));

            // Maps
            CreateMap<CreateTestWithoutQuestionsDTO, Test>()
                .ForMember(d => d.Title, a => a.MapFrom(t => t.Title))
                .ForMember(d => d.TestCode, a => a.MapFrom(t => t.TestCode))
                .ForMember(d => d.TargetUser, a => a.MapFrom(t => t.TargetUser))
                .ForMember(d => d.Description, a => a.MapFrom(t => t.Description))
                .ForMember(d => d.Price, a => a.MapFrom(t => t.Price))
                .ForMember(d => d.AuthorId, a => a.MapFrom(t => t.AuthorId))
                .ForMember(d => d.TestCategoryId, a => a.MapFrom(t => t.TestCategoryId))
                .ForMember(d => d.SpecializationId, a => a.MapFrom(t => t.SpecializationId));

            CreateMap<Test, TestOverviewResponseDTO>()
                .ForMember(d => d.Title, a => a.MapFrom(t => t.Title))
                .ForMember(d => d.TestCode, a => a.MapFrom(t => t.TestCode))
                .ForMember(d => d.TargetUser, a => a.MapFrom(t => t.TargetUser))
                .ForMember(d => d.Description, a => a.MapFrom(t => t.Description))
                .ForMember(d => d.Price, a => a.MapFrom(t => t.Price))
                .ForMember(d => d.Author, a => a.MapFrom(t => t.Author))
                .ForMember(d => d.TestStatus, a => a.MapFrom(t => t.TestStatus))
                .ForMember(d => d.TestCategory, a => a.MapFrom(t => t.TestCategory))
                .ForMember(d => d.Specialization, a => a.MapFrom(t => t.Specialization));

            CreateMap<TestDraft, Test>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Title, a => a.MapFrom(t => t.Title))
                .ForMember(d => d.TestCode, a => a.MapFrom(t => t.TestCode != null ? t.TestCode : null))
                .ForMember(d => d.Description, a => a.MapFrom(t => t.Description))
                .ForMember(d => d.Price, a => a.MapFrom(t => t.Price))
                .ForMember(d => d.AuthorId, a => a.MapFrom(t => t.AuthorId))
                .ForMember(d => d.TestCategoryId, a => a.MapFrom(t => t.TestCategoryId))
                .ForMember(d => d.SpecializationId, a => a.MapFrom(t => t.SpecializationId))
                .ForMember(d => d.TargetUser, a => a.MapFrom(t => t.TargetUser));
        }
    }
}
