using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.Appointments.Commands.CancelBookingAppointment;
using MindSpace.Application.Features.Appointments.Commands.ConfirmBookingAppointment;
using MindSpace.Application.Features.Appointments.Commands.HandleWebhook;
using Stripe.Checkout;

namespace MindSpace.API.Controllers
{
    public class AppointmentController : BaseApiController
    {
        private readonly IMediator _mediator;
        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("booking/confirm")]
        public async Task<IActionResult> ConfirmBookingAppointment([FromBody] ConfirmBookingAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("booking/cancel")]
        public async Task<IActionResult> CancelBookingAppointment([FromBody] CancelBookingAppointmentCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("booking/webhook/{link}")]
        public async Task<IActionResult> HandleWebhook(string link)
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var command = new HandleWebhookCommand
            {
                StripeEventJson = json
            };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("booking/expire-session/{sessionId}")]
        public async Task<IActionResult> ExpireSession([FromRoute] string sessionId)
        {
            var session = new SessionService();
            await session.ExpireAsync(sessionId);

            return Ok("Session expired");
        }
    }
}