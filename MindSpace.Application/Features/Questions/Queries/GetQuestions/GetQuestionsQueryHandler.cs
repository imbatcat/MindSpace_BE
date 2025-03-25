using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.QuestionSpecifications;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.Questions.Queries.GetQuestions;

public class GetQuestionsQueryHandler(
    ILogger<GetQuestionsQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetQuestionsQuery, PagedResultDTO<QuestionResponseDTO>>
{
    public async Task<PagedResultDTO<QuestionResponseDTO>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get list of Questions with Spec: {@Spec}", request.SpecParams);

        var spec = new QuestionSpecification(request.SpecParams);

        // Use Projection map to DTO
        var listDto = await unitOfWork.Repository<Question>().GetAllWithSpecProjectedAsync<QuestionResponseDTO>(spec, mapper.ConfigurationProvider);

        var count = await unitOfWork
             .Repository<Question>()
             .CountAsync(spec);

        return new PagedResultDTO<QuestionResponseDTO>(count, listDto);
    }
}