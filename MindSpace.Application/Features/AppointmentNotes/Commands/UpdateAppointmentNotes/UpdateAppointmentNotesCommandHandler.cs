using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.AppointmentNotes.Commands.UpdateAppointmentNotes
{
    public class UpdateAppointmentNotesCommandHandler : IRequestHandler<UpdateAppointmentNotesCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAppointmentNotesCommandHandler> _logger;

        // constructor
        public UpdateAppointmentNotesCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UpdateAppointmentNotesCommandHandler> logger
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task Handle(UpdateAppointmentNotesCommand request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await _unitOfWork.Repository<Appointment>().GetBySpecAsync(new AppointmentSpecification(request.AppointmentId));

            if (appointment == null)
            {
                throw new NotFoundException("Appointment not found!");
            }
            if (appointment.Status != AppointmentStatus.Success)
            {
                throw new ForbiddenException();
            }
            appointment.NotesTitle = request.NotesTitle;
            appointment.KeyIssues = request.KeyIssues;
            appointment.Suggestions = request.Suggestions;
            appointment.OtherNotes = request.OtherNotes;
            appointment.IsNoteShown = request.IsNoteShown;

            _unitOfWork.Repository<Appointment>().Update(appointment);
            await _unitOfWork.CompleteAsync();

        }
    }
}
