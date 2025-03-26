using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.GetAllPsychologistsNames
{
    internal class GetAllPsychologistsNamesQueryHandler(
        IApplicationUserService<Psychologist> _applicationUserService,
        ILogger<GetAllPsychologistsNamesQueryHandler> _logger
        ) : IRequestHandler<GetAllPsychologistsNamesQuery, List<string>>
    {
        public async Task<List<string>> Handle(GetAllPsychologistsNamesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all psychologists names.");
            var allPsychologists = await _applicationUserService.GetUsersByRoleAsync(UserRoles.Psychologist);

            var allPsychologistsNames = allPsychologists.Select(x => x.FullName).ToList();

            _logger.LogInformation("Total psychologists {count}.", allPsychologistsNames.Count);

            return allPsychologistsNames!;
        }
    }
}
