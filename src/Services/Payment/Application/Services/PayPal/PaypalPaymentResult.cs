using Application.Abstractions.Services;

namespace Application.Services.PayPal;

public record PaypalPaymentResult : IPaymentResult
{
    public bool Success { get; init; }
    public int Code { get; init; }
    public string Message { get; init; }
    public Guid TransactionId { get; init; }
}