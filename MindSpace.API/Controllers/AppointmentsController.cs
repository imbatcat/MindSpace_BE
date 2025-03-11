using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.Features.Appointments.Commands.CancelBookingAppointment;
using MindSpace.Application.Features.Appointments.Commands.ConfirmBookingAppointment;
using MindSpace.Application.Features.Appointments.Commands.HandleWebhook;
using Stripe.Checkout;

namespace MindSpace.API.Controllers;

public class AppointmentsController(IMediator mediator) : BaseApiController
{
    // ==============================
    // === POST, PUT, DELETE, PATCH
    // ==============================

    // POST /api/appointments/booking/confirm
    [HttpPost("booking/confirm")]
    public async Task<IActionResult> ConfirmBookingAppointment([FromBody] ConfirmBookingAppointmentCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    // POST /api/appointments/booking/cancel
    [HttpPost("booking/cancel")]
    public async Task<IActionResult> CancelBookingAppointment([FromBody] CancelBookingAppointmentCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    // POST /api/appointments/booking/webhook/{link}
    [HttpPost("bookings/webhook/{link}")]
    public async Task<IActionResult> HandleWebhook(string link)
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var command = new HandleWebhookCommand
        {
            StripeEventJson = json
        };
        await mediator.Send(command);
        return Ok();
    }

    // ==============================
    // === GET
    // ==============================

    // GET /api/appointments/booking/expire-session/{sessionId}
    [Cache(600)]
    [HttpGet("booking/expire-session/{sessionId}")]
    public async Task<IActionResult> ExpireSession([FromRoute] string sessionId)
    {
        var session = new SessionService();
        await session.ExpireAsync(sessionId);

        return Ok("Session expired");
    }
}