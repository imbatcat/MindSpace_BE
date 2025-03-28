using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.TestSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Tests.Commands.ToggleTestStatus
{
    public class ToggleTestStatusCommandHandler(
    IUnitOfWork _unitOfWork,
    ILogger<ToggleTestStatusCommandHandler> _logger) : IRequestHandler<ToggleTestStatusCommand>
    {
        public async Task Handle(ToggleTestStatusCommand request, CancellationToken cancellationToken)
        {
            int testId = request.TestId;
            _logger.LogInformation("Toggling test status for test {TestId}", testId);
            var test = await _unitOfWork.Repository<Test>().GetBySpecAsync(new TestSpecification(testId));
            if (test == null)
            {
                throw new NotFoundException(nameof(Test), testId.ToString());
            }

            test.TestStatus = test.TestStatus == TestStatus.Enabled ? TestStatus.Disabled : TestStatus.Enabled;
            _unitOfWork.Repository<Test>().Update(test);
            await _unitOfWork.CompleteAsync();
        }
    }
}
