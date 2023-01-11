namespace Application.Abstractions.Services;

public interface IPaymentResult
{
    bool Success { get; }
    int Code { get; }
    string Message { get; }
    Guid TransactionId { get; }
}