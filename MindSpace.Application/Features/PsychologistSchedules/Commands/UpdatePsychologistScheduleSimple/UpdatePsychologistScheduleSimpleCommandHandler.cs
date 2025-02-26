using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.PsychologistScheduleSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.Features.PsychologistSchedules.Commands.UpdatePsychologistScheduleSimple
{
    public class UpdatePsychologistScheduleSimpleCommandHandler : IRequestHandler<UpdatePsychologistScheduleSimpleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePsychologistScheduleSimpleCommandHandler> _logger;
        public UpdatePsychologistScheduleSimpleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdatePsychologistScheduleSimpleCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdatePsychologistScheduleSimpleCommand request, CancellationToken cancellationToken)
        {
            // check only current user can update his/her own schedules
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
            foreach(var slot in existedSlots)
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
