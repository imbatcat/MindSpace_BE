using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.BackgroundJobs.SupportingPrograms;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.SchoolSpecifications;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.CreateSupportingProgram
{
    public class CreateSupportingProgramCommandHandler(
        ILogger<CreateSupportingProgramCommandHandler> logger,
            IUnitOfWork unitOfWork,
            IBackgroundJobService backgroundJobService,
            IMapper mapper)
        : IRequestHandler<CreateSupportingProgramCommand, SupportingProgramResponseDTO>
    {
        // ================================
        // === Methods
        // ================================

        public async Task<SupportingProgramResponseDTO> Handle(CreateSupportingProgramCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Create Supporting Program with Title: {@title}", request.Title);

            var spec = new SupportingProgramSpecification(request.Title);
            var existingProgram = await unitOfWork.Repository<SupportingProgram>().GetBySpecAsync(spec);

            // If existed then throw exception
            if (existingProgram != null) throw new DuplicateObjectException($"Supporting Program with title {request.Title} exists");

            // Update or throw exception
            var spToCreate = mapper.Map<SupportingProgram>(request);

            // Get School from schoolManagerId
            var school = await unitOfWork.Repository<School>().GetBySpecAsync(new SchoolSpecifications(spToCreate.SchoolManagerId));
            spToCreate.SchoolId = school.Id;

            // Add to the database
            var addedSP = unitOfWork.Repository<SupportingProgram>().Insert(spToCreate)
                ?? throw new UpdateFailedException(nameof(SupportingProgram));

            await unitOfWork.CompleteAsync();

            // Set the Supporting Program to a live in 7 days only
            await ScheduleSetInActiveAfterStartDateJob(addedSP);

            return mapper.Map<SupportingProgram, SupportingProgramResponseDTO>(addedSP);
        }

        private async Task ScheduleSetInActiveAfterStartDateJob(SupportingProgram sp)
        {
            var jobKey = $"{nameof(SetInActiveAfterStartDateJob)}-{Guid.NewGuid()}";
            var deactivationDate = sp.StartDateAt.AddDays(7);
            var jobData = new Dictionary<string, object>
            {
                ["ProgramId"] = sp.Id
            };
            await backgroundJobService.ScheduleJobWithFireOnce<SetInActiveAfterStartDateJob>(jobKey, deactivationDate, jobData);
        }
    }
}
