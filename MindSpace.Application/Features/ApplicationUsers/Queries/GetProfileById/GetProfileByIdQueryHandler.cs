﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Specifications;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Application.Specifications.PsychologistsSpecifications;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.GetProfileById
{
    public class GetProfileByIdQueryHandler(
        ILogger<GetProfileByIdQueryHandler> logger,
        IApplicationUserService<ApplicationUser> applicationUserService,
        IMapper mapper
    ) : IRequestHandler<GetProfileByIdQuery, ApplicationUserProfileDTO>
    {
        public async Task<ApplicationUserProfileDTO> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting user profile for user ID: {UserId}", request.UserId);

            var spec = new ApplicationUserSpecification(request.UserId);

            var user = await applicationUserService.GetUserWithSpec(spec);

            if (user == null)
            {
                logger.LogWarning("User not found with ID: {UserId}", request.UserId);
                return null!;
            }

            // If user is the psychologist
            if (user is Psychologist psychologist)
            {
                logger.LogInformation("Mapping psychologist profile for user {Email}", user.Email);
                return mapper.Map<PsychologistProfileDTO>(user);
            }

            logger.LogInformation("Mapping regular user profile for user {Email}", user.Email);
            return mapper.Map<ApplicationUserProfileDTO>(user);
        }
    }
}
