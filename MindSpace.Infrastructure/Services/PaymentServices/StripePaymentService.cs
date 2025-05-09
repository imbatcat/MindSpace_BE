﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Commons.Constants;
using MindSpace.Application.Interfaces.Services.PaymentServices;
using Stripe.Checkout;
using static MindSpace.Application.Commons.Constants.AppCts.StripePayment;

namespace MindSpace.Infrastructure.Services.PaymentServices
{
    internal class StripePaymentService(
        IConfiguration _configuration,
        ILogger<StripePaymentService> _logger
    ) : IStripePaymentService
    {

        public (string, string) CreateCheckoutSession(decimal sessionPrice)
        {
            var priceData = new SessionLineItemPriceDataOptions()
            {
                Currency = AppCts.StripePayment.PaymentCurrency,
                UnitAmount = (long)(sessionPrice),
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
            };
            var service = new SessionService();
            Session session = service.Create(options);

            _logger.LogInformation("Stripe session created: {SessionId}, Status: {Status}", session.Id, session.Status);
            return (session.Id, session.Url);
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

        public async Task<string> RetrieveSessionUrlAsync(string sessionId)
        {
            var service = new SessionService();
            var checkoutSession = await service.GetAsync(sessionId);
            return checkoutSession.Url;
        }
    }
}