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

namespace MindSpace.Application.Features.Tests.Commands.CreateTestImport
{
    public class CreateTestImportCommandHandler : IRequestHandler<CreateTestImportCommand, TestOverviewResponseDTO>
    {
        // ================================
        // === Fields & Props
        // ================================
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateTestImportCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IExcelReaderService _excelReaderService;
        private readonly ITestImportService _testImportService;


        // ================================
        // === Constructors
        // ================================
        public CreateTestImportCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateTestImportCommandHandler> logger, IMapper mapper, IExcelReaderService excelReaderService, ITestImportService testImportService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _excelReaderService = excelReaderService;
            _testImportService = testImportService;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<TestOverviewResponseDTO> Handle(CreateTestImportCommand request, CancellationToken cancellationToken)
        {
            // Validate input file
            if (request.TestFile == null || request.TestFile.Length == 0)
            {
                throw new BadHttpRequestException("File Excel không được để trống.");
            }

            // Insert test with overview data
            var testInfo = request.TestInfo;
            var testEntity = _mapper.Map<Test>(testInfo);

            // Check existed PSYCHOLOGY test
            var existedTest = await _unitOfWork.Repository<Test>()
                .GetBySpecAsync(new TestSpecification(testInfo.Title,
                                             testInfo.AuthorId,
                                             testInfo.TestCode));
            if (existedTest != null)
            {
                throw new DuplicateObjectException("The title or test code is duplcated with an existed test!");
            }

            // Get data of test file
            var fileData = await _testImportService.ReadAndValidateTestFileAsync(request.TestFile);

            // Handle Psy Options
            _testImportService.InsertPsychologyTestOptions(testEntity, fileData[CreateTestImportConstants.PSYCHOLOGY_TEST_OPTION_SHEET]);

            // Handle Questions
            _testImportService.InsertQuestions(testEntity, fileData[CreateTestImportConstants.QUESTION_SHEET]);
            testEntity.QuestionCount = testEntity.TestQuestions.Count;

            // Handle Score Ranks
            _testImportService.InsertScoreRanks(testEntity, fileData[CreateTestImportConstants.SCORE_RANK_SHEET]);

            _unitOfWork.Repository<Test>().Insert(testEntity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<TestOverviewResponseDTO>(testEntity);

        }
    }


}
