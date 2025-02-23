using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Resources.Commands.CreateResourceAsArticle
{
    public class CreatedResourceAsArticleCommandHandler : IRequestHandler<CreatedResourceAsArticleCommand>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<CreatedResourceAsArticleCommand> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================

        public CreatedResourceAsArticleCommandHandler(ILogger<CreatedResourceAsArticleCommand> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ================================
        // === Methods
        // ================================

        public async Task Handle(CreatedResourceAsArticleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create Resource with Title: {@title}", request.Title);

            var articleToCreate = _mapper.Map<Resource>(request);
            _ = _unitOfWork.Repository<Resource>().Insert(articleToCreate)
                ?? throw new UpdateFailedException(nameof(Resource));

            await _unitOfWork.CompleteAsync();
        }
    }
}
