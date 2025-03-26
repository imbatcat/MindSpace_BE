using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Services.FileReaderServices;
using MindSpace.Application.Specifications.TestSpecifications;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Tests.Commands.CreateTestImport;

public class CreateTestImportCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<CreateTestImportCommandHandler> logger,
    IMapper mapper,
    IExcelReaderService excelReaderService,
    ITestImportService testImportService) : IRequestHandler<CreateTestImportCommand, TestOverviewResponseDTO>
{
    public async Task<TestOverviewResponseDTO> Handle(CreateTestImportCommand request, CancellationToken cancellationToken)
    {
        // Validate input file
        if (request.TestFile == null || request.TestFile.Length == 0)
        {
            throw new BadHttpRequestException("File Excel không được để trống.");
        }

        // Insert test with overview data
        var testInfo = request.TestInfo;
        var testEntity = mapper.Map<Test>(testInfo);

        // Check existed test
        var existedTest = await unitOfWork.Repository<Test>()
            .GetBySpecAsync(new TestSpecification(testInfo.TestCode));
        if (existedTest != null)
        {
            throw new DuplicateObjectException("The test code is duplicated with an existed test!");
        }

        // Get data of test file
        var fileData = await testImportService.ReadAndValidateTestFileAsync(request.TestFile);

        // Handle Psy Options
        testImportService.InsertPsychologyTestOptions(testEntity, fileData[CreateTestImportConstants.PSYCHOLOGY_TEST_OPTION_SHEET]);

        // Handle Questions
        testImportService.InsertQuestions(testEntity, fileData[CreateTestImportConstants.QUESTION_SHEET]);
        testEntity.QuestionCount = testEntity.TestQuestions.Count;

        // Handle Score Ranks
        testImportService.InsertScoreRanks(testEntity, fileData[CreateTestImportConstants.SCORE_RANK_SHEET]);

        unitOfWork.Repository<Test>().Insert(testEntity);
        await unitOfWork.CompleteAsync();

        return mapper.Map<TestOverviewResponseDTO>(testEntity);
    }
}