using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.Payments.Commands.CancelPayment;
using MindSpace.Application.Features.Payments.Commands.CreatePayment;
using MindSpace.Application.Features.Payments.Commands.UpdatePaymentWithWebhook;
using Net.payOS.Types;
using Newtonsoft.Json;

namespace MindSpace.API.Controllers
{
    public class PaymentController(IMediator mediator, ILogger<PaymentController> logger, IMapper mapper) : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand command)
        {
            var paymentResult = await mediator.Send(command);
            return Ok(paymentResult);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> HandleWebhook()
        {
            var body = await new StreamReader(Request.Body).ReadToEndAsync();
            var webhookData = JsonConvert.DeserializeObject<WebhookType>(body);

            logger.LogInformation("Payment webhook received: {Body}", webhookData);
            await mediator.Send(new UpdatePaymentWithWebhookCommand()
            {
                Code = webhookData.code,
                Desc = webhookData.desc,
                Data = webhookData,
                Signature = webhookData.signature
            });
            return Ok();
        }

        [HttpPost("{paymentId}/cancel")]
        public async Task<IActionResult> CancelPayment([FromRoute] int paymentId)
        {
            await mediator.Send(new CancelPaymentCommand
            {
                PaymentId = paymentId,
            });
            return Ok();
        }


    }
}