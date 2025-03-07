using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Exceptions;
using Stripe;
using Stripe.Checkout;

namespace MindSpace.Application.Features.Appointments.Commands.HandleWebhook;

internal class HandleWebhookCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<HandleWebhookCommandHandler> logger) : IRequestHandler<HandleWebhookCommand>
{
    public async Task Handle(HandleWebhookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var stripeEvent = EventUtility.ParseEvent(request.StripeEventJson);

            switch (stripeEvent.Type)
            {
                case EventTypes.CheckoutSessionCompleted:
                    var session = stripeEvent.Data.Object as Session;
                    logger.LogInformation("Checkout session completed: {0}", session);
                    // await HandleCompletedSessionAsync(session!.Id);
                    break;
                case EventTypes.CheckoutSessionExpired:
                    var expiredSession = stripeEvent.Data.Object as Session;
                    await HandleExpiredSessionAsync(expiredSession!.Id);
                    //Call SignalR to notify the client that the session has expired
                    break;
                default:
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error handling webhook event: {0}", ex.Message);
            throw;
        }

        async Task HandleExpiredSessionAsync(string sessionId)
        {
            var specification = new AppointmentSpecification(sessionId);
            var appointment = await unitOfWork.Repository<Appointment>().GetBySpecAsync(specification)
                ?? throw new NotFoundException(nameof(Appointment), sessionId);

            appointment.Status = AppointmentStatus.Failed;
            appointment.PsychologistSchedule.Status = PsychologistScheduleStatus.Free;

            unitOfWork.Repository<Appointment>().Update(appointment);
            unitOfWork.Repository<PsychologistSchedule>().Update(appointment.PsychologistSchedule);
            await unitOfWork.CompleteAsync();
        }

        async Task HandleCompletedSessionAsync(string sessionId)
        {
            var specification = new AppointmentSpecification(sessionId);
            var appointment = await unitOfWork.Repository<Appointment>().GetBySpecAsync(specification)
                ?? throw new NotFoundException(nameof(Appointment), sessionId);

            appointment.Status = AppointmentStatus.Success;
            appointment.PsychologistSchedule.Status = PsychologistScheduleStatus.Booked;

            unitOfWork.Repository<Appointment>().Update(appointment);
            unitOfWork.Repository<PsychologistSchedule>().Update(appointment.PsychologistSchedule);
            await unitOfWork.CompleteAsync();
        }
    }
}