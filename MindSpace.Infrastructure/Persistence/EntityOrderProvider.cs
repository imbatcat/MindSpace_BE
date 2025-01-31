using MindSpace.Application.Commons.Utilities.Seeding;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Persistence
{
    internal class EntityOrderProvider : IEntityOrderProvider
    {
        public IEnumerable<string> GetOrderedEntities() => new[]
        {
            typeof(SupportingProgramHistory).FullName!,
            typeof(SupportingProgram).FullName!,
            typeof(Feedback).FullName!,
            typeof(TestResponseItem).FullName!,
            typeof(TestResponse).FullName!,

            typeof(Psychologist).FullName!,
            typeof(Manager).FullName!,
            typeof(Student).FullName!,

            typeof(ApplicationUser).FullName!,
            typeof(ApplicationRole).FullName!,

            typeof(TestQuestionOption).FullName!,
            typeof(Test).FullName!,

            typeof(ResourceSection).FullName!,
            typeof(Resource).FullName!,
            typeof(TestQuestion).FullName!,
            typeof(QuestionOption).FullName!,
            typeof(TestScoreRank).FullName!,

            typeof(School).FullName!,
            typeof(Specialization).FullName!,
            typeof(TestCategory).FullName!,
            typeof(Appointment).FullName!,
            typeof(Payment).FullName!,
            typeof(PsychologistSchedule).FullName!,
            typeof(QuestionCategory).FullName!
        };
    }
}