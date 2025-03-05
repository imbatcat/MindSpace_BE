using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services.PaymentServices;
using Stripe;
using Stripe.Checkout;

namespace MindSpace.Infrastructure.Services.PaymentServices
{
    internal class StripePaymentService : IStripePaymentService
    {
        private readonly string _paymentCurrency = "vnd";
        private readonly IConfiguration _configuration;
        private readonly ILogger<StripePaymentService> _logger;
        public StripePaymentService(IConfiguration configuration, ILogger<StripePaymentService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }


        public (string sessionId, string sessionUrl) CreateCheckoutSession(decimal sessionPrice, decimal commisionRate)
        {
            var priceData = new SessionLineItemPriceDataOptions()
            {
                Currency = _paymentCurrency,
                UnitAmount = (long)(sessionPrice * commisionRate) + 200000,
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
                ExpiresAt = DateTime.Now.AddMinutes(int.Parse(_configuration["Stripe:SessionExpirationMinutes"]!))
            };

            var service = new SessionService();
            Session session = service.Create(options);

            _logger.LogInformation("Stripe session created: {SessionId}, Status: {Status}", session.Id, session.Status);
            //think of what to save here
            return (session.Id, session.Url);
        }
    }
}