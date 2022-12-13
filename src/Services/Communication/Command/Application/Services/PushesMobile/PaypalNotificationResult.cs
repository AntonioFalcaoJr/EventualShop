using Application.Abstractions.Services;

namespace Application.Services.PushesMobile;

public record PaypalNotificationResult : INotificationResult
{
    public bool Success { get; init; }
    public int Code { get; init; }
    public string Message { get; init; }
    public Guid TransactionId { get; init; }
}