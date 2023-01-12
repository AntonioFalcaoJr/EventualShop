using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Payment;

public static class Projection
{
    public record Payment(Guid Id, Guid OrderId, decimal Amount, Dto.Address BillingAddress,
        IEnumerable<Dto.PaymentMethod> PaymentMethods, string Status, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.Payment(Payment payment)
            => new()
            {
                Id = payment.Id.ToString(),
                OrderId = payment.OrderId.ToString(),
                Amount = (double) payment.Amount,
                Address = payment.BillingAddress,
                Status = payment.Status,
            };
    }

    public record PaymentMethod(Guid Id, Guid PaymentId, decimal Amount, Dto.IPaymentOption? Option, string Status,
        bool IsDeleted) : IProjection
    {
        public static implicit operator Abstractions.Protobuf.PaymentMethod(PaymentMethod paymentMethod)
            => new()
            {
                Amount = (double) paymentMethod.Amount,
                Id = paymentMethod.Id.ToString(),
                PaymentId = paymentMethod.Id.ToString(),
                Status = paymentMethod.Status,
            };
    }
}