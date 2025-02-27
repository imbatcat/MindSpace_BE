using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services;
using Stripe;
using Stripe.Checkout;

namespace MindSpace.Infrastructure.Services.PaymentServices
{
    internal class StripePaymentService : IStripePaymentService
    {
        private readonly string _paymentCurrency = "vnd";
        private readonly IConfiguration _configuration;
        private readonly ILogger<StripePaymentService> _logger;
        private readonly string _domain;
        private readonly int _sessionExpirationMinutes;
        public StripePaymentService(IConfiguration configuration, ILogger<StripePaymentService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _domain = _configuration["Stripe:Domain"] ?? throw new InvalidOperationException("Stripe:Domain configuration is missing");
            _sessionExpirationMinutes = _configuration.GetValue<int>("Stripe:SessionExpirationMinutes");
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
                SuccessUrl = _domain + "?success=true",
                CancelUrl = _domain + "?canceled=true",
                ExpiresAt = DateTime.Now.AddMinutes(_sessionExpirationMinutes)
            };

            var service = new SessionService();
            Session session = service.Create(options);

            //think of what to save
            return (session.Id, session.Url);
        }
    }
}