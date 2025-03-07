using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
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
    ILogger<RegisterSupportingProgramCommandHandler> logger) : IRequestHandler<RegisterSupportingProgramCommand>
{
    public async Task Handle(RegisterSupportingProgramCommand request, CancellationToken cancellationToken)
    {
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
            JoinedAt = DateTime.UtcNow,
        };

        // Check if student already register this supporting program or not 
        var historySpec = new SupportingProgramHistorySpecification(existingStudent.Id, existingSupportingProgram.Id);
        var historySP = await unitOfWork.Repository<SupportingProgramHistory>().GetBySpecAsync(historySpec);
        if (historySP != null) throw new DuplicateObjectException("Student already register this supporting program");

        // Commit changes
        _ = unitOfWork.Repository<SupportingProgramHistory>().Insert(supportingProgramHistory)
            ?? throw new CreateFailedException(nameof(SupportingProgramHistory));

        await unitOfWork.CompleteAsync();
    }
}
