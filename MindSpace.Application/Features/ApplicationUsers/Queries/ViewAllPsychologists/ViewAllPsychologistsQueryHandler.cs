using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.ViewAllPsychologists
{
    internal class ViewAllPsychologistsQueryHandler(
        IApplicationUserRepository applicationUserRepository,
        ILogger<ViewAllPsychologistsQueryHandler> _logger) : IRequestHandler<ViewAllPsychologistsQuery, List<string>>
    {
        public async Task<List<string>> Handle(ViewAllPsychologistsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all psychologists name");
            var psychologists = await applicationUserRepository.GetUsersByRoleAsync(UserRoles.Psychologist);

            var psychologistNames = psychologists.Select(x => x.FullName).ToList();

            return psychologistNames!;
        }
    }
}
