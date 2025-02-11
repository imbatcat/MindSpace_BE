namespace MindSpace.Infrastructure.Persistence;

using Application.Commons.Utilities.Seeding;
using Domain.Entities;
using Domain.Entities.Appointments;
using Domain.Entities.Identity;
using Domain.Entities.SupportingPrograms;
using Domain.Entities.Tests;
using MindSpace.Domain.Entities.Resources;

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
        typeof(SchoolManager).FullName!,
        typeof(Student).FullName!,

        typeof(ApplicationUser).FullName!,
        typeof(ApplicationRole).FullName!,

        typeof(TestQuestion).FullName!,
        typeof(Test).FullName!,

        typeof(ResourceSection).FullName!,
        typeof(Resource).FullName!,
        typeof(Question).FullName!,
        typeof(QuestionOption).FullName!,
        typeof(TestScoreRank).FullName!,

        typeof(School).FullName!,
        typeof(Specialization).FullName!,
        typeof(TestCategory).FullName!,
        typeof(Appointment).FullName!,
        typeof(Payment).FullName!,
        typeof(PsychologistSchedule).FullName!,
        typeof(TestPublication).FullName!,
        typeof(PsychologyTestOption).FullName!
    };
}
