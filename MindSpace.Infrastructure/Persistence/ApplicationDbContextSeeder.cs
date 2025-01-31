using MindSpace.Application.Commons.Constants;
using MindSpace.Application.Commons.Utilities;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Infrastructure.Seeders;

namespace MindSpace.Infrastructure.Persistence
{
    public class ApplicationDbContextSeeder
    {
        // =====================================
        // === Props & Fields
        // =====================================

        private readonly IFileReader _fileReader;
        private readonly IDataSeeder _dataSeeder;
        private readonly ApplicationDbContext _dbContext;

        // =====================================
        // === Constructors
        // =====================================

        public ApplicationDbContextSeeder(IFileReader fileReader, ApplicationDbContext dbContext, IDataSeeder dataSeeder)
        {
            _fileReader = fileReader;
            _dbContext = dbContext;
            _dataSeeder = dataSeeder;
        }

        // =====================================
        // === Methods
        // =====================================

        public async Task SeedAllAsync()
        {
            //await new JsonDataSeeder<TestQuestion>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.QuestionSeeder).SeedAsync();
            //await new JsonDataSeeder<School>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.SchoolSeeder).SeedAsync();
            //await new JsonDataSeeder<Test>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.Test).SeedAsync();
            //await new JsonDataSeeder<TestCategory>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestCategory).SeedAsync();
            //await new JsonDataSeeder<TestQuestionOption>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestQuestionOption).SeedAsync();
            //await new JsonDataSeeder<TestResponse>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestResponse).SeedAsync();

            //await new JsonDataSeeder<PsychologistSchedule>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.PsychologistScheduleSeeder).SeedAsync();
            //await new JsonDataSeeder<Appointment>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.AppointmentSeeder).SeedAsync();
            //await new JsonDataSeeder<Payment>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.PaymentSeeder).SeedAsync();
            //await new JsonDataSeeder<QuestionOption>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.QuestionOptionSeeder).SeedAsync();
            //await new JsonDataSeeder<TestResponseItem>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestResponseItemSeeder).SeedAsync();
            //await new JsonDataSeeder<TestScoreRank>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestScoreRankSeeder).SeedAsync();
        }
    }
}