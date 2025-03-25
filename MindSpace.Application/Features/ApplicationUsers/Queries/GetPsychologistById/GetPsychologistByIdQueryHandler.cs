using AutoMapper;
using MediatR;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.PsychologistsSpecifications;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.GetPsychologistById
{
    public class GetPsychologistByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IApplicationUserService<Psychologist> applicationUserService)
        : IRequestHandler<GetPsychologistByIdQuery, PsychologistProfileDTO>
    {
        async Task<PsychologistProfileDTO> IRequestHandler<GetPsychologistByIdQuery, PsychologistProfileDTO>.Handle(GetPsychologistByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new PsychologistSpecification(request.Id);
            var psy = await applicationUserService.GetUserWithSpec(spec)
                ?? throw new NotFoundException($"Psychologist with Id {request.Id} not found.");

            return mapper.Map<Psychologist, PsychologistProfileDTO>(psy);
        }
    }
}
