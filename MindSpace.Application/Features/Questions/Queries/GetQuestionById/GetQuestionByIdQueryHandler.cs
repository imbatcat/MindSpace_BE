using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.QuestionSpecifications;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Questions.Queries.GetQuestionById;

public class GetQuestionByIdQueryHandler(
    ILogger<GetQuestionByIdQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetQuestionByIdQuery, QuestionResponseDTO>
{
    public async Task<QuestionResponseDTO> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get Question By Id: {@Id}", request.Id);

        var spec = new QuestionSpecification(request.Id);

        var dataDto = await unitOfWork
            .Repository<Question>()
            .GetBySpecProjectedAsync<QuestionResponseDTO>(spec, mapper.ConfigurationProvider);

        if (dataDto == null)
        {
            throw new NotFoundException(nameof(Question), request.Id.ToString());
        }

        return dataDto;
    }
}
