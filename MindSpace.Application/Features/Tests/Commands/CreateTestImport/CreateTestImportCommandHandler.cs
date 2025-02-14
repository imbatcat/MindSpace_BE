using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Services;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;
using Newtonsoft.Json;

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
            var testEntity = _mapper.Map<Test>(request.TestInfo);
            _unitOfWork.Repository<Test>().Insert(testEntity);

            // Get data of test file
            var fileData = await _testImportService.ReadAndValidateTestFileAsync(request.TestFile);

            // Handle Psy Options
            _testImportService.InsertPsychologyTestOptions(testEntity, fileData[CreateTestImportConstants.PSYCHOLOGY_TEST_OPTION_SHEET]);

            // Handle Questions
            _testImportService.InsertQuestions(testEntity, fileData[CreateTestImportConstants.QUESTION_SHEET]);

            // Handle Score Ranks
            _testImportService.InsertScoreRanks(testEntity, fileData[CreateTestImportConstants.SCORE_RANK_SHEET]);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<TestOverviewResponseDTO>(testEntity);

        }

    }


}
