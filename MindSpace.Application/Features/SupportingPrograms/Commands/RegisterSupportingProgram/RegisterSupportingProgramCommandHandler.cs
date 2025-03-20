using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.BackgroundJobs.SupportingPrograms;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Application.Specifications.SupportingProgramHistorySpecifications;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.RegisterSupportingProgram;

public class RegisterSupportingProgramCommandHandler(
    IUnitOfWork unitOfWork,
    IApplicationUserRepository userService,
    IBackgroundJobService backgroundJobService,
    ILogger<RegisterSupportingProgramCommandHandler> logger) : IRequestHandler<RegisterSupportingProgramCommand>
{
    public async Task Handle(RegisterSupportingProgramCommand request, CancellationToken cancellationToken)
    {
        // Get Supporting Program and Student Information
        var suppportingProgramSpec = new SupportingProgramSpecification(request.SupportingProgramId);
        var existingSupportingProgram = await unitOfWork.Repository<SupportingProgram>().GetBySpecAsync(suppportingProgramSpec)
            ?? throw new NotFoundException(nameof(SupportingProgram), request.SupportingProgramId.ToString());

        var studentSpec = new ApplicationUserSpecification(request.StudentId);
        var existingStudent = await userService.GetUserWithSpec(studentSpec)
            ?? throw new NotFoundException(nameof(Student), request.SupportingProgramId.ToString());

        // Inserting references
        var supportingProgramHistory = new SupportingProgramHistory()
        {
            SupportingProgramId = existingSupportingProgram.Id,
            StudentId = existingStudent.Id,
            JoinedAt = DateTime.Now,
        };

        // Check if student already register this supporting program or not 
        var historySpec = new SupportingProgramHistorySpecification(existingStudent.Id, existingSupportingProgram.Id);
        var historySP = await unitOfWork.Repository<SupportingProgramHistory>().GetBySpecAsync(historySpec);
        if (historySP != null) throw new DuplicateObjectException("Student already register this supporting program");

        // Commit changes
        _ = unitOfWork.Repository<SupportingProgramHistory>().Insert(supportingProgramHistory)
            ?? throw new CreateFailedException(nameof(SupportingProgramHistory));

        await unitOfWork.CompleteAsync();

        // Set the job to notify the starting date of supporting program for registered user
        await SetReminderForSupportingProgram(existingSupportingProgram, existingStudent);
    }

    private async Task SetReminderForSupportingProgram(SupportingProgram sp, ApplicationUser student)
    {
        // Extra information for email service
        Dictionary<string, object> jobDatas = new Dictionary<string, object>();
        jobDatas.Add("UserId", student.Id);
        jobDatas.Add("UserEmail", student.Email);
        jobDatas.Add("StartDateAt", sp.StartDateAt);

        // Configure the job
        await backgroundJobService.ScheduleJobWithFireOnce<NotifyRegisteredUserJob>(
            $"NotifyRegisteredUserJob-{sp.Id}-{student.Id}",
            sp.StartDateAt,
            jobDatas
        );
    }
}
