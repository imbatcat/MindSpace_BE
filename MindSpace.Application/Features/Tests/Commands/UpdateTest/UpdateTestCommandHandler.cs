using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.TestSpecifications;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand>
    {

        private readonly ILogger<UpdateTestCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITestDraftService _testDraftService;

        public UpdateTestCommandHandler(ILogger<UpdateTestCommandHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ITestDraftService testDraftService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _testDraftService = testDraftService;
        }

        public async Task Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            // get existed test to update
            Test? existedTest = await _unitOfWork.Repository<Test>().GetBySpecAsync(new TestSpecification(request.TestId));
            if (existedTest == null)
            {
                throw new NotFoundException(nameof(Test), request.TestId.ToString());
            }

            _unitOfWork.Repository<Test>().Delete(existedTest);

            var testDraftId = request.TestDraftId;
            var testDraft = await _testDraftService.GetTestDraftAsync(testDraftId);

            // Check each field in the test draft to see any missing data
            if (testDraft == null) throw new NotFoundException(nameof(TestDraft), testDraftId);

            // check existed test with test code
            var existedTestWithTestCode = await _unitOfWork.Repository<Test>()
            .GetBySpecAsync(new TestSpecification(testDraft.TestCode));

            // Map test dto to entity
            _mapper.Map<TestDraft, Test>(testDraft, existedTest);

            // TestQuestions
            if (request.IsModifiedQuestions)
            {
                existedTest.TestQuestions.Clear();
                HandleAddQuestions(testDraft, existedTest);
            }

            _unitOfWork.Repository<Test>().Update(existedTest);
            await _unitOfWork.CompleteAsync();

            // remove test draft from redis
            await _testDraftService.DeleteTestDraftAsync(testDraftId);
        }
        private void HandleAddQuestions(TestDraft testDraft, Test testUpdated)
        {
            List<TestQuestion> newQuestions = new List<TestQuestion>();
            // Add questions to table
            foreach (var questionDraft in testDraft.QuestionItems)
            {
                if (questionDraft.IsNewQuestion)
                {
                    // if new question => create new question
                    Question questionToAdd = _mapper.Map<QuestionDraft, Question>(questionDraft);
                    // question option is auto-mapped with question
                    var newTestWithNewQuestion = new TestQuestion { Question = questionToAdd, Test = testUpdated };
                    // Add question to test
                    testUpdated.TestQuestions.Add(newTestWithNewQuestion);
                }
                else
                {
                    var newTestQuestionWithOldQuestion = new TestQuestion
                    {
                        QuestionId = questionDraft.Id,
                        TestId = testUpdated.Id
                    };

                    if (!testUpdated.TestQuestions.Contains(newTestQuestionWithOldQuestion))
                    {
                        // Add question to test
                        testUpdated.TestQuestions.Add(newTestQuestionWithOldQuestion);
                    }
                }
            }
        }
    }
}
