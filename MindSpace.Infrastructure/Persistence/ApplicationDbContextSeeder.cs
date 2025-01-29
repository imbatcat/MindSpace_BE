using MindSpace.Application.Commons.Constants;
using MindSpace.Application.Commons.Utilities;
using MindSpace.Domain.Entities;
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
            await new JsonDataSeeder<TestQuestion>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.QuestionSeeder).SeedAsync();
            await new JsonDataSeeder<School>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.SchoolSeeder).SeedAsync();
            await new JsonDataSeeder<Test>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.Test).SeedAsync();
            await new JsonDataSeeder<TestCategory>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestCategory).SeedAsync();
            await new JsonDataSeeder<TestQuestionOption>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestQuestionOption).SeedAsync();
            await new JsonDataSeeder<TestResponse>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.TestResponse).SeedAsync();
        }
    }
}