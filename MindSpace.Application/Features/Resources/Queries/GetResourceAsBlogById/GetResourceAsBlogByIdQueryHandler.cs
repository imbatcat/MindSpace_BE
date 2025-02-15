using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Specifications.ResourceSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;

namespace MindSpace.Application.Features.Resources.Queries.GetResourceAsBlogById
{
    public class GetResourceAsBlogByIdQueryHandler : IRequestHandler<GetResourceAsBlogByIdQuery, BlogResponseDTO>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetResourceAsBlogByIdQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================

        public GetResourceAsBlogByIdQueryHandler(ILogger<GetResourceAsBlogByIdQueryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<BlogResponseDTO> Handle(GetResourceAsBlogByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Get Blog: {request.Id}");

            var spec = new ResourceSpecification(request.Id, ResourceType.Blog);
            var blog = await _unitOfWork
                .Repository<Resource>()
                .GetBySpecProjectedAsync<BlogResponseDTO>(spec, _mapper.ConfigurationProvider);

            if (blog == null)
                throw new NotFoundException(nameof(Resource), request.Id.ToString());

            return _mapper.Map<BlogResponseDTO>(blog);
        }
    }
}
