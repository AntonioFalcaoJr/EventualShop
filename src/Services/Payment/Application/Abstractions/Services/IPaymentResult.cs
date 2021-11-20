using System;

namespace Application.Abstractions.Services;

public interface IPaymentResult
{
    public bool Success { get; init; }
    public int Code { get; init; }
    public string Message { get; init; }
    public Guid TransactionId { get; init; }
}