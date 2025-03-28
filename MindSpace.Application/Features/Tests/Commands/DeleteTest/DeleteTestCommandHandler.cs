using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.TestResponseSpecifications;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.Tests.Commands.DeleteTest
{
    public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand>
    {
        private readonly ILogger<DeleteTestCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteTestCommandHandler(ILogger<DeleteTestCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteTestCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Delete Test By Id: {@Id}", request.TestId);

            var spec = new TestResponseSpecification(request.TestId, true);
            bool isAnswered = (await _unitOfWork.Repository<TestResponse>().GetBySpecAsync(spec) != null);
            if (isAnswered)
            {
                throw new BadHttpRequestException("Cannot delete this test because it has published and answered by users!");
            }
            _unitOfWork.Repository<Test>().Delete(request.TestId);
            await _unitOfWork.CompleteAsync();
        }
    }
}
