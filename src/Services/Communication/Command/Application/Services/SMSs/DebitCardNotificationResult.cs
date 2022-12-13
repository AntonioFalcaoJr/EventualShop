using Application.Abstractions.Services;

namespace Application.Services.SMSs;

public record DebitCardNotificationResult : INotificationResult
{
    public bool Success { get; init; }
    public int Code { get; init; }
    public string Message { get; init; }
    public Guid TransactionId { get; init; }
}