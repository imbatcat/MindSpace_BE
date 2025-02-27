using Microsoft.Extensions.Logging;
using MindSpace.Application.Commons.Constants;
using MindSpace.Application.Interfaces.Utilities;
using MindSpace.Application.Interfaces.Utilities.Seeding;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Entities.SupportingPrograms;
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
        private readonly IIdentitySeeder _identitySeeder;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<ApplicationDbContextSeeder> _logger;

        // =====================================
        // === Constructors
        // =====================================

        public ApplicationDbContextSeeder(
            IFileReader fileReader,
            ApplicationDbContext dbContext,
            IIdentitySeeder identitySeeder,
            ILogger<ApplicationDbContextSeeder> logger,
            ILoggerFactory loggerFactory)
        {
            _fileReader = fileReader;
            _dbContext = dbContext;
            _identitySeeder = identitySeeder;
            _loggerFactory = loggerFactory;
            _logger = logger;
        }

        // =====================================
        // === Methods
        // =====================================

        public async Task SeedAllAsync()
        {

            await new JsonDataSeeder<Specialization>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<Specialization>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.SpecializationSeeder)
                .SeedAsync();

            await new JsonDataSeeder<School>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<School>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.SchoolSeeder)
                .SeedAsync();

            await new JsonDataSeeder<TestCategory>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<TestCategory>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestCategorySeeder)
                .SeedAsync();

            await _identitySeeder.SeedAsync();

            await new JsonDataSeeder<Test>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<Test>>(),
                _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestSeeder)
                .SeedAsync();
            await new JsonDataSeeder<TestScoreRank>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<TestScoreRank>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestScoreRankSeeder)
                .SeedAsync();
            await new JsonDataSeeder<Question>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<Question>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.QuestionSeeder)
                .SeedAsync();
            await new JsonDataSeeder<PsychologyTestOption>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<PsychologyTestOption>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.PsychologyTestOptionSeeder)
                .SeedAsync();
            await new JsonDataSeeder<QuestionOption>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<QuestionOption>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.QuestionOptionSeeder)
                .SeedAsync();
            await new JsonDataSeeder<TestQuestion>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<TestQuestion>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestQuestionSeeder)
                .SeedAsync();
            await new JsonDataSeeder<TestResponse>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<TestResponse>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestResponseSeeder)
                .SeedAsync();

            await new JsonDataSeeder<TestResponseItem>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<TestResponseItem>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestResponseItemSeeder)
                .SeedAsync();

            await new JsonDataSeeder<PsychologistSchedule>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<PsychologistSchedule>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.PsychologistScheduleSeeder)
                .SeedAsync();

            await new JsonDataSeeder<Appointment>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<Appointment>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.AppointmentSeeder)
                .SeedAsync();

            await new JsonDataSeeder<Invoice>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<Invoice>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.PaymentSeeder)
                .SeedAsync();




            await new JsonDataSeeder<SupportingProgram>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<SupportingProgram>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.SupportingProgramSeeder)
                .SeedAsync();

            await new JsonDataSeeder<SupportingProgramHistory>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<SupportingProgramHistory>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.SupportingProgramHistorySeeder)
                .SeedAsync();

            await new JsonDataSeeder<Feedback>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<Feedback>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.FeedbackSeeder)
                .SeedAsync();

            await new JsonDataSeeder<Resource>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<Resource>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.ResourceSeeder)
                .SeedAsync();

            await new JsonDataSeeder<ResourceSection>(_fileReader, _loggerFactory.CreateLogger<JsonDataSeeder<ResourceSection>>(), _dbContext)
                .AddRelativeFilePath(AppCts.Locations.RelativeFilePath.ResourceSectionsSeeder)
                .SeedAsync();
        }
    }
}