using Application.Abstractions.Handlers;

namespace Application.Handlers.PushesMobile;

public record PaypalNotificationResult : INotificationResult
{
    public bool Success { get; init; }
    public int Code { get; init; }
    public string Message { get; init; }
    public Guid TransactionId { get; init; }
}