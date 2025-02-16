using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.DTOs;
using MindSpace.Application.Features.Resources.Queries.GetArticles;
using MindSpace.Application.Specifications.ResourceSpecifications;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Resources.Queries.GetBlogs
{
    public class GetBlogsQueryHandler
    : IRequestHandler<GetBlogsQuery, PagedResultDTO<BlogResponseDTO>>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetBlogsQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================
        public GetBlogsQueryHandler(
            ILogger<GetBlogsQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<PagedResultDTO<BlogResponseDTO>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of Blogs: {@spec}", request.SpecParams);
            var spec = new ResourceSpecification(request.SpecParams);

            // Use Projection 
            var listDto = await _unitOfWork
                .Repository<Resource>()
                .GetAllWithSpecProjectedAsync<BlogResponseDTO>(spec, _mapper.ConfigurationProvider);

            var count = await _unitOfWork
                .Repository<Resource>()
                .CountAsync(spec);

            return new PagedResultDTO<BlogResponseDTO>(count, listDto);
        }
    }
}
