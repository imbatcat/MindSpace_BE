using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Commons.Constants;
using MindSpace.Application.Interfaces.Services.PaymentServices;
using Stripe.Checkout;
using static MindSpace.Application.Commons.Constants.BusinessCts.StripePayment;

namespace MindSpace.Infrastructure.Services.PaymentServices
{
    internal class StripePaymentService(
        IConfiguration _configuration,
        ILogger<StripePaymentService> _logger
    ) : IStripePaymentService
    {

        public string CreateCheckoutSession(decimal sessionPrice, decimal commissionRate)
        {
            var priceData = new SessionLineItemPriceDataOptions()
            {
                Currency = BusinessCts.StripePayment.PaymentCurrency,
                UnitAmount = (long)(sessionPrice * commissionRate),
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = "Appointment",
                    Description = "Appointment with psychologist"
                }
            };
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    PriceData = priceData,
                    Quantity = 1
                  },
                },
                Mode = "payment",
                SuccessUrl = _configuration["Stripe:SuccessUrl"],
                CancelUrl = _configuration["Stripe:CancelUrl"],
            };

            var service = new SessionService();
            Session session = service.Create(options);

            _logger.LogInformation("Stripe session created: {SessionId}, Status: {Status}", session.Id, session.Status);
            return session.Id;
        }

        public async Task ExpireStripeCheckoutSession(string sessionId)
        {
            var service = new SessionService();
            
            var session = await service.GetAsync(sessionId);
            _logger.LogInformation("Stripe session status: {Status}", session.Status);
            if (session.Status == StripeCheckoutSessionStatus.open.ToString())
            {
                await service.ExpireAsync(sessionId);
            }
        }
    }
}