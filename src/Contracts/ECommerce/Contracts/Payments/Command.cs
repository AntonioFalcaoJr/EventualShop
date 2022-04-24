using ECommerce.Abstractions.Messages;
using ECommerce.Abstractions.Messages.Commands;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Payments;

public static class Command
{
    public record RequestPayment(Guid OrderId, decimal AmountDue, Models.Address BillingAddress, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Message(CorrelationId: OrderId), ICommand;

    public record CancelPayment(Guid PaymentId, Guid OrderId) : Message(CorrelationId: PaymentId), ICommand;

    public record UpdatePaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId, bool Authorized) : Message(CorrelationId: PaymentId), ICommand;

    public record ProceedWithPayment(Guid PaymentId, Guid OrderId) : Message(CorrelationId: PaymentId), ICommand;
}