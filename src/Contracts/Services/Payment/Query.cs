using Contracts.Abstractions.Messages;

namespace Contracts.Services.Payment;

public static class Query
{
    public record struct GetPayment(Guid PaymentId) : IQuery
    {
        public static implicit operator GetPayment(Protobuf.GetPaymentRequest request)
            => new(new(request.PaymentId));
    }
}