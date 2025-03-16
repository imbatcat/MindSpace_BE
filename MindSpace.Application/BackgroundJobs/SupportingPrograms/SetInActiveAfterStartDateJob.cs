using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;
using Quartz;

namespace MindSpace.Application.BackgroundJobs.SupportingPrograms
{
    public class SetInActiveAfterStartDateJob(
        IUnitOfWork unitOfWork,
        ILogger<SetInActiveAfterStartDateJob> logger
        ) : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                logger.LogInformation("Starting to set supporting program inactive after start date");

                // Get data from job context
                var dataMap = context.JobDetail.JobDataMap;
                var programId = dataMap.GetInt("ProgramId");

                // Get the supporting programToUpdate
                var spec = new SupportingProgramSpecification(programId);
                var programToUpdate = await unitOfWork.Repository<SupportingProgram>().GetBySpecAsync(spec)
                    ?? throw new NotFoundException(nameof(SupportingProgram), programId.ToString());

                // Set programToUpdate to inactive
                programToUpdate.IsActive = false;
                programToUpdate.UpdateAt = DateTime.Now;

                // Update in database
                unitOfWork.Repository<SupportingProgram>().Update(programToUpdate);
                await unitOfWork.CompleteAsync();

                logger.LogInformation("Successfully set supporting program {ProgramId} to inactive", programId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error setting supporting program to inactive");
                throw new JobExecutionException(ex);
            }
        }
    }
}
