using ECommerce.Abstractions.Messages.Commands;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Payment;

public static class Commands
{
    public record RequestPayment(Guid OrderId, decimal AmountDue, Models.Address BillingAddress, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Command(CorrelationId: OrderId);

    public record CancelPayment(Guid PaymentId, Guid OrderId) : Command(CorrelationId: PaymentId);

    public record UpdatePaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId, bool Authorized) : Command(CorrelationId: PaymentId);

    public record ProceedWithPayment(Guid PaymentId, Guid OrderId) : Command(CorrelationId: PaymentId);
}