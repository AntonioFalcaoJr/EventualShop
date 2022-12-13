namespace Application.Abstractions.Services;

public interface INotificationResult
{
    bool Success { get; }
    int Code { get; }
    string Message { get; }
    Guid TransactionId { get; }
}