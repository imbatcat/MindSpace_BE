using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;

namespace MindSpace.Application.Features.Tests.Commands.CreateTestManual
{
    public class CreateTestManualCommandHandler : IRequestHandler<CreateTestManualCommand, TestOverviewResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateTestManualCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITestDraftService _testDraftService;
        public CreateTestManualCommandHandler(
                IUnitOfWork unitOfWork,
                ILogger<CreateTestManualCommandHandler> logger,
                IMapper mapper,
                ITestDraftService testDraftService
            )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _testDraftService = testDraftService;
        }

        public async Task<TestOverviewResponseDTO> Handle(CreateTestManualCommand request, CancellationToken cancellationToken)
        {
            var testDraftId = request.TestDraftId;
            var testDraft = await _testDraftService.GetTestDraftAsync(testDraftId);

            // Check each field in the test draft to see any missing data
            if (testDraft == null) throw new NotFoundException(nameof(TestDraft), testDraftId);

            // Add test to table
            var testToAdd = _mapper.Map<TestDraft, Test>(testDraft);

            // TestQuestions
            testToAdd.TestQuestions = new List<TestQuestion>();

            // Add questions to table
            foreach (var questionDraft in testDraft.QuestionItems)
            {
                var questionToAdd = _mapper.Map<QuestionDraft, Question>(questionDraft);
                foreach (var optionDraft in questionDraft.QuestionOptions)
                {
                    var optionToAdd = _mapper.Map<OptionDraft, QuestionOption>(optionDraft);
                    questionToAdd.QuestionOptions.Add(optionToAdd);
                }

                // Add question to test
                testToAdd.TestQuestions.Add(new TestQuestion { Question = questionToAdd, Test = testToAdd });
            }

            // question count
            testToAdd.QuestionCount = testToAdd.TestQuestions.Count;

            _unitOfWork.Repository<Test>().Insert(testToAdd);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<Test, TestOverviewResponseDTO>(testToAdd);
        }
    }
}
