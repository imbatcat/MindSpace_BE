using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services.EmailServices;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Exceptions;
using Quartz;
using MediatR;
using MindSpace.Application.Features.MeetingRooms.Commands.CreateMeetingRoom;
using MindSpace.Application.Features.MeetingRooms.Commands.UpdateMeetingRoom;
using MindSpace.Application.Features.Appointments.Commands.UpdateBookingAppointment;

namespace MindSpace.Application.BackgroundJobs.MeetingRooms;

public class CreateMeetingRoomJob(
    ILogger<CreateMeetingRoomJob> _logger,
    IUnitOfWork _unitOfWork,
    IEmailService _emailService,
    IConfiguration _configuration,
    IMediator _mediator
) : IJob
{
    private readonly string _meetUrl = _configuration.GetValue<string>("MeetUrl")!;
    public async Task Execute(IJobExecutionContext context)
    {
        // Handle the creation of a meeting room by retrieving the appointment details, creating the meeting room, updating the appointment with the meeting room ID, and sending notifications to relevant parties.
        // Flow: 
        // 1. Retrieve the session ID from the job data.
        // 2. Get the appointment using the session ID.
        // 3. Create a command to create a meeting room with the appointment's start date.
        // 4. Send the command to create the meeting room and retrieve the room ID.
        // 5. Update the appointment with the newly created meeting room ID.
        // 6. Send notifications to the student and psychologist about the created meeting room.

        _logger.LogInformation("Starting {bgJob}", nameof(CreateMeetingRoomJob));
        var referenceId = context.JobDetail.JobDataMap.GetString("referenceId");
        var sessionId = referenceId!.Split("-")[1];
        var startDate = context.JobDetail.JobDataMap.GetDateTime("AppointmentTime");

        var appointment = await GetAppointment(sessionId!);

        var command = new CreateMeetingRoomCommand()
        {
            StartDate = startDate,
            Appointment = appointment
        };

        _logger.LogInformation("Start creating meeting room");
        var newMeetingRoom = await _mediator.Send(command);
        _logger.LogInformation("Finished creating meeting room");

        appointment.MeetingRoomId = newMeetingRoom.Id;

        _logger.LogInformation("Start updating booking appointment {AppointmentId}", appointment.Id);

        await _mediator.Send(new UpdateBookingAppointmentCommand()
        {
            Appointment = appointment
        });

        _logger.LogInformation("Finished updating booking appointment {AppointmentId}", appointment.Id);

        _logger.LogInformation("Start sending room created notifications");
        await SendRoomCreatedNotifications(appointment, newMeetingRoom.RoomId);
        _logger.LogInformation("Finished sending room created notifications");

        _logger.LogInformation("Finished {bgJob}", nameof(CreateMeetingRoomJob));

        #region Helper Methods
        async Task<Appointment> GetAppointment(string sessionId)
        {
            var specification = new AppointmentSpecification(sessionId, AppointmentSpecification.StringParameterType.SessionId);
            return await _unitOfWork.Repository<Appointment>().GetBySpecAsync(specification) ??
                throw new NotFoundException(typeof(Appointment).Name, sessionId);
        }

        async Task SendRoomCreatedNotifications(Appointment appointment, string roomId)
        {
            // Email to student 
            await _emailService.SendEmailAsync(appointment.Student.Email!, "Meeting Room Created", $"Your meeting room has been created. Click here to join: <a href='{_meetUrl}/{roomId}'>Join Meeting</a>");

            // Email to psychologist
            await _emailService.SendEmailAsync(appointment.Psychologist.Email!, "Meeting Room Created", $"Your meeting room has been created. Click here to join: <a href='{_meetUrl}/{roomId}'>Join Meeting</a>");
        }
        #endregion
    }
}
