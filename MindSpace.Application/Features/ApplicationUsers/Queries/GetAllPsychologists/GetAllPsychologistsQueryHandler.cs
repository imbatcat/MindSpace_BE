using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Application.Specifications.PsychologistsSpecifications;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.GetAllPsychologists
{
    public class GetAllPsychologistsQueryHandler(
        ILogger<GetAllPsychologistsQueryHandler> logger,
        IApplicationUserService<Psychologist> applicationUserService,
        IMapper mapper
    ) : IRequestHandler<GetAllPsychologistsQuery, PagedResultDTO<PsychologistProfileDTO>>
    {
        public async Task<PagedResultDTO<PsychologistProfileDTO>> Handle(GetAllPsychologistsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all Psychologists accounts");

            var specification = new PsychologistSpecification(request.SpecParams, false);
            var psychologists = await applicationUserService.GetAllUsersWithSpecAsync(specification);

            logger.LogInformation("Found {Count} student users", psychologists.Count);
            var psychologistsDtos = mapper.Map<List<PsychologistProfileDTO>>(psychologists);

            return new PagedResultDTO<PsychologistProfileDTO>(psychologistsDtos.Count, psychologistsDtos);
        }
    }
}