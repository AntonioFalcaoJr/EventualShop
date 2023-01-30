using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Payment;

public static class Projection
{
    public record PaymentDetails(Guid Id, Guid OrderId, Dto.Money Amount, string Status, bool IsDeleted, long Version) : IProjection
    {
        public static implicit operator Protobuf.PaymentDetails(PaymentDetails payment)
            => new()
            {
                Id = payment.Id.ToString(),
                OrderId = payment.OrderId.ToString(),
                Amount = payment.Amount,
                Status = payment.Status
            };
    }

    public record PaymentMethodDetails(Guid Id, Guid OrderId, Dto.Money Amount, Dto.IPaymentOption Option, string Status, bool IsDeleted, long Version) : IProjection
    {
        public static implicit operator Protobuf.PaymentMethodDetails(PaymentMethodDetails method)
            => new()
            {
                Id = method.Id.ToString(),
                OrderId = method.OrderId.ToString(),
                Amount = method.Amount,
                Status = method.Status,
                Option = method.Option switch
                {
                    Dto.CreditCard creditCard => new() { CreditCard = creditCard },
                    Dto.DebitCard debitCard => new() { DebitCard = debitCard },
                    Dto.PayPal payPal => new() { PayPal = payPal },
                    _ => default
                }
            };
    }

    public record PaymentMethodListItem(Guid Id, Guid OrderId, Dto.Money Amount, string Option, string Status, bool IsDeleted, long Version) : IProjection
    {
        public static implicit operator Protobuf.PaymentMethodListItem(PaymentMethodListItem method)
            => new()
            {
                Id = method.Id.ToString(),
                OrderId = method.OrderId.ToString(),
                Amount = method.Amount,
                Status = method.Status,
                Option = method.Option
            };
    }
}