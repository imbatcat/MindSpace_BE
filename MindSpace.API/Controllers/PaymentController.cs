using MediatR;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MindSpace.Application.Features.Payments.Commands.CancelPayment;
using MindSpace.Application.Features.Payments.Commands.CreatePayment;
using MindSpace.Application.Features.Payments.Commands.UpdatePaymentWithWebhook;
using Net.payOS.Types;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;
using Session = Stripe.Checkout.Session;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using SessionService = Stripe.Checkout.SessionService;
using Stripe.Treasury;
using MindSpace.Infrastructure.Services.SignalR;

namespace MindSpace.API.Controllers
{
    public class PaymentController(
        IMediator mediator,
        ILogger<PaymentController> logger,
        IHubContext<NotificationHub> hubContext
        ) : BaseApiController
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

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateSession([FromRoute] int paymentId)
        {
            var domain = "http://localhost:4242";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                      Price = "price_1QvVSFJxbmUKw0RKTDh5fuy5",
                      Quantity = 2
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + "?success=true",
                CancelUrl = domain + "?canceled=true",
                ExpiresAt = DateTime.Now.AddMinutes(30),
            };

            var MapPaymentStatus = new PaymentIntentService();
            var service = new SessionService();
            Session session = service.Create(options);

            //think of what to save
            //Response.Headers.Add("Location", session.Url);
            return Ok(session.Url);
        }

        [HttpGet("create-checkout-session")]
        public async Task<IActionResult> GetPrice([FromRoute] int paymentId)
        {
            var priceService = new PriceService();
            var res = await priceService.GetAsync("price_1QvVSFJxbmUKw0RKTDh5fuy5");
            logger.LogInformation(res.UnitAmount.ToString());

            return Ok(res);
        }

        [HttpPost("stripe-webhook/{signature}")]
        public async Task<IActionResult> Index(string signature)
        {
            logger.LogInformation(signature);
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);

                // Handle the event
                // If on SDK version < 46, use class Events instead of EventTypes
                if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    // Then define and call a method to handle the successful payment intent.
                    // handlePaymentIntentSucceeded(paymentIntent);
                }
                else if (stripeEvent.Type == EventTypes.CheckoutSessionCompleted)
                {
                    var paymentMethod = stripeEvent.Data.Object as Session;

                    logger.LogInformation("checkout success {map}", paymentMethod.PaymentIntentId);
                    // Then define and call a method to handle the successful attachment of a PaymentMethod.
                    // handlePaymentMethodAttached(paymentMethod);
                }
                else if (stripeEvent.Type == EventTypes.CheckoutSessionExpired)
                {
                    logger.LogInformation("checkout expired");
                }
                else if (stripeEvent.Type == EventTypes.PaymentIntentCanceled)
                {
                    logger.LogInformation("cancelled");
                }
                else if (stripeEvent.Type == EventTypes.PaymentIntentRequiresAction)
                {
                    logger.LogInformation("require action");
                }
                // ... handle other event types
                else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}