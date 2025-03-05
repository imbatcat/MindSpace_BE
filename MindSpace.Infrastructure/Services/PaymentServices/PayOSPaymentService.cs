using Microsoft.Extensions.Configuration;
using MindSpace.Application.DTOs;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services.PaymentServices;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using Net.payOS;
using Net.payOS.Types;
using System.Security.Cryptography;
using System.Text;

namespace MindSpace.Infrastructure.Services.PaymentServices;

public class PayOSPaymentService : IPaymentService
{
    private readonly PayOS _payOS;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _checksumKey;
    private readonly long _expiredAt;

    public PayOSPaymentService(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _unitOfWork = unitOfWork;

        var clientId = configuration["payOSSettings:ClientId"];
        var apiKey = configuration["payOSSettings:ApiKey"];
        _checksumKey = configuration["payOSSettings:ChecksumKey"]!;
        _expiredAt = long.Parse(configuration["payOSSettings:ExpiredAt"]!);

        _payOS = new PayOS(clientId!, apiKey!, _checksumKey);
    }

    public async Task<CreatePaymentResult> CreatePaymentLinkAsync(
        int amount,
        string description,
        List<ItemData> items,
        int appointmentId,
        int accountId)
    {
        var domain = _configuration["FrontendUrl"];
        var cancelUrlQueryString = _configuration["payOSSettings:CancelUrlQueryString"];
        var returnUrlQueryString = _configuration["payOSSettings:ReturnUrlQueryString"];

        var orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
        var cancelUrl = $"{domain}?{cancelUrlQueryString}";
        var returnUrl = $"{domain}?{returnUrlQueryString}";

        var signature = GenerateSignature(amount, cancelUrl, description, orderCode, returnUrl);

        var paymentLinkRequest = new PaymentData(
            orderCode: orderCode,
            amount: amount,
            description: description,
            items: items,
            cancelUrl: cancelUrl,
            returnUrl: returnUrl,
            expiredAt: DateTimeOffset.UtcNow.ToUnixTimeSeconds() + _expiredAt,
            signature: signature
        );

        var response = await _payOS.createPaymentLink(paymentLinkRequest);
        await SavePaymentAsync(appointmentId, accountId, amount, orderCode, description);

        return response;
    }

    public async Task CancelPaymentLinkAsync(int paymentId)
    {
        await _payOS.cancelPaymentLink(paymentId);
    }

    public async Task<Invoice> SavePaymentAsync(
        int appointmentId,
        int accountId,
        int amount,
        int transactionCode,
        string description)
    {
        var payment = new Invoice
        {
            AppointmentId = appointmentId,
            AccountId = accountId,
            Amount = amount,
            TransactionCode = transactionCode,
            Provider = "payOS",
            PaymentMethod = PaymentMethod.MOMO,
            PaymentDescription = description,
            CreateAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow,
            PaymentType = PaymentType.Purchase,
            TransactionTime = null
        };

        _unitOfWork.Repository<Invoice>().Insert(payment);
        await _unitOfWork.CompleteAsync();

        return payment;
    }

    public async Task<PaymentWebhookResponse> VerifyWebhookDataAsync(WebhookType webhookData)
    {
        var verifiedData = _payOS.verifyPaymentWebhookData(webhookData);
        return await Task.FromResult(new PaymentWebhookResponse
        {
            OrderCode = verifiedData.orderCode.ToString(),
            Status = verifiedData.code,
            Amount = verifiedData.amount,
            TransactionTime = DateTime.Parse(verifiedData.transactionDateTime)
        });
    }

    public async Task UpdatePaymentFromWebhookAsync(Invoice payment, PaymentWebhookResponse webhookData)
    {
        payment.TransactionTime = webhookData.TransactionTime;
        payment.UpdateAt = DateTime.UtcNow;

        _unitOfWork.Repository<Invoice>().Update(payment);
        await _unitOfWork.CompleteAsync();
    }

    private string GenerateSignature(int amount, string cancelUrl, string description, int orderCode, string returnUrl)
    {
        var hashSource = $"amount={amount}&cancelUrl={cancelUrl}&description={description}&orderCode={orderCode}&returnUrl={returnUrl}";
        var hash = HMACSHA256.HashData(Encoding.UTF8.GetBytes(_checksumKey), Encoding.UTF8.GetBytes(hashSource));
        return Convert.ToBase64String(hash);
    }
}