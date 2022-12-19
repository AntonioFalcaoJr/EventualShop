namespace Application.Abstractions.Handlers;

public interface INotificationResult
{
    bool Success { get; }
    int Code { get; }
    string Message { get; }
    Guid TransactionId { get; }
}