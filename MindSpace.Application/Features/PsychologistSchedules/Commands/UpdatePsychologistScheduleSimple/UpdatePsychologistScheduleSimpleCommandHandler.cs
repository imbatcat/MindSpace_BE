using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Application.Specifications.PsychologistScheduleSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.PsychologistSchedules.Commands.UpdatePsychologistScheduleSimple
{
    public class UpdatePsychologistScheduleSimpleCommandHandler : IRequestHandler<UpdatePsychologistScheduleSimpleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePsychologistScheduleSimpleCommandHandler> _logger;
        private readonly IUserContext _userContext;
        private readonly IApplicationUserService<ApplicationUser> _applicationUserService;
        public UpdatePsychologistScheduleSimpleCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UpdatePsychologistScheduleSimpleCommandHandler> logger,
            IUserContext userContext,
            IApplicationUserService<ApplicationUser> applicationUserRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _userContext = userContext;
            _applicationUserService = applicationUserRepository;
        }

        public async Task Handle(UpdatePsychologistScheduleSimpleCommand request, CancellationToken cancellationToken)
        {
            // check only current user can update his/her own schedules

            var userClaims = _userContext.GetCurrentUser();
            ApplicationUser? currentUser = _applicationUserService.GetUserByEmailAsync(userClaims!.Email).Result;
            if (currentUser == null || currentUser.Id != request.PsychologistId)
            {
                throw new AuthorizationFailedException("You are not authorized to update schedule of this psychologist!");
            }

            var specParams = new PsychologistScheduleSpecParams
            {
                PsychologistId = request.PsychologistId,
                MinDate = request.StartDate,
                MaxDate = request.EndDate,
                Status = PsychologistScheduleStatus.Free
            };
            var existedSlots = _unitOfWork.Repository<PsychologistSchedule>()
                                .GetAllWithSpecAsync(new PsychologistScheduleSpecification(specParams))
                                .Result;
            foreach (var slot in existedSlots)
            {
                _unitOfWork.Repository<PsychologistSchedule>().Delete(slot.Id);
            }

            foreach (TimeSlotDTO slot in request.Timeslots)
            {
                var newSlot = _mapper.Map<PsychologistSchedule>(slot);
                newSlot.PsychologistId = request.PsychologistId;
                newSlot.Status = PsychologistScheduleStatus.Free;
                _unitOfWork.Repository<PsychologistSchedule>().Insert(newSlot);
            }
            await _unitOfWork.CompleteAsync();
        }
    }

}
