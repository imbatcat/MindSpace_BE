using MediatR;
using Microsoft.VisualBasic;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Application.Specifications.SupportingProgramHistorySpecifications;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.UnregisterSupportingProgram
{
    public class UnregisterSupportingProgramCommandHandler(
        IUnitOfWork unitOfWork,
        IApplicationUserRepository userService,
        IBackgroundJobService backgroundJobService
    ) : IRequestHandler<UnregisterSupportingProgramCommand>
    {
        public async Task Handle(UnregisterSupportingProgramCommand request, CancellationToken cancellationToken)
        {
            // Find already registerd student
            var spec = new SupportingProgramHistorySpecification(request.StudentId, request.SupportingProgramId);
            var registeredSupportingProgram = await unitOfWork.Repository<SupportingProgramHistory>().GetBySpecAsync(spec)
                ?? throw new NotFoundException("Student does not register the supporting program");

            // Remove the records
            unitOfWork.Repository<SupportingProgramHistory>().Delete(registeredSupportingProgram);
            await unitOfWork.CompleteAsync();

            // Remove the background job that assign to reminder user
            await UnassignReminderForThisStudent(request.SupportingProgramId, request.StudentId);
        }

        private async Task UnassignReminderForThisStudent(int supportingProgramId, int studentId)
        {
            await backgroundJobService.UnscheduleJob($"NotifyRegisteredUserJob-{supportingProgramId}-{studentId}");
        }
    }
}
