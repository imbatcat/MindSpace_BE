using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.Tests.Commands.CreateTestImport;
using MindSpace.Application.Services;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Application.Specifications.StudentSpecification;
using MindSpace.Application.Specifications.SupportingProgramHistorySpecifications;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.RegisterSupportingProgram
{
    public class RegisterSupportingProgramCommandHandler : IRequestHandler<RegisterSupportingProgramCommand>
    {
        // ================================
        // === Fields & Props
        // ================================

        private IUnitOfWork _unitOfWork;
        private IApplicationUserService _userService;
        private readonly ILogger<RegisterSupportingProgramCommandHandler> _logger;

        // ================================
        // === Constructors
        // ================================
        public RegisterSupportingProgramCommandHandler(IUnitOfWork unitOfWork, IApplicationUserService userService, ILogger<RegisterSupportingProgramCommandHandler> logger)
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
