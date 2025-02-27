using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services.Authentication;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Application.Specifications.SupportingProgramHistorySpecifications;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.RegisterSupportingProgram
{
    public class RegisterSupportingProgramCommandHandler : IRequestHandler<RegisterSupportingProgramCommand>
    {
        // ================================
        // === Fields & Props
        // ================================

        private IUnitOfWork _unitOfWork;
        private IApplicationUserRepository _userService;
        private readonly ILogger<RegisterSupportingProgramCommandHandler> _logger;

        // ================================
        // === Constructors
        // ================================
        public RegisterSupportingProgramCommandHandler(IUnitOfWork unitOfWork, IApplicationUserRepository userService, ILogger<RegisterSupportingProgramCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _logger = logger;
        }

        // ================================
        // === Methods
        // ================================

        public async Task Handle(RegisterSupportingProgramCommand request, CancellationToken cancellationToken)
        {
            var suppportingProgramSpec = new SupportingProgramSpecification(request.SupportingProgramId);
            var existingSupportingProgram = await _unitOfWork.Repository<SupportingProgram>().GetBySpecAsync(suppportingProgramSpec)
                ?? throw new NotFoundException(nameof(SupportingProgram), request.SupportingProgramId.ToString());

            var studentSpec = new ApplicationUserSpecification(request.StudentId);
            var existingStudent = await _userService.GetUserWithSpec(studentSpec)
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
            var historySP = await _unitOfWork.Repository<SupportingProgramHistory>().GetBySpecAsync(historySpec);
            if (historySP != null) throw new ResourceAlreadyExistsException(nameof(SupportingProgramHistory));

            // Commit changes
            _ = _unitOfWork.Repository<SupportingProgramHistory>().Insert(supportingProgramHistory)
                ?? throw new CreateFailedException(nameof(SupportingProgramHistory));

            await _unitOfWork.CompleteAsync();
        }
    }
}
