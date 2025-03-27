using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.ApplicationUsers.Commands.UpdatePsychologistCommissionRate
{
    internal class UpdatePsychologistCommissionRateCommandHandler(
        ILogger<UpdatePsychologistCommissionRateCommandHandler> logger,
        IApplicationUserService<ApplicationUser> applicationUserService) : IRequestHandler<UpdatePsychologistCommissionRateCommand>
    {
        public async Task Handle(UpdatePsychologistCommissionRateCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Attempting to update commission rate for psychologist with ID: {PsychologistId}", request.PsychologistId);

            var user = await applicationUserService.GetByIdAsync(request.PsychologistId);
            if (user == null)
            {
                logger.LogWarning("Psychologist not found with ID: {PsychologistId}", request.PsychologistId);
                throw new NotFoundException(nameof(Psychologist), request.PsychologistId.ToString());
            }

            if (user is not Psychologist psychologist)
            {
                logger.LogWarning("User with ID {UserId} is not a psychologist", request.PsychologistId);
                throw new Exception("The specified user is not a psychologist");
            }

            logger.LogInformation("Updating commission rate for psychologist {PsychologistId} from {OldRate} to {NewRate}",
                request.PsychologistId, psychologist.ComissionRate, request.CommissionRate);

            psychologist.ComissionRate = request.CommissionRate;

            await applicationUserService.UpdateAsync(psychologist);
            logger.LogInformation("Successfully updated commission rate for psychologist with ID: {PsychologistId}", request.PsychologistId);
        }
    }
}