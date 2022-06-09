using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Payment;

public static class Command
{
    public record RequestPayment(Guid OrderId, decimal AmountDue, Dto.Address BillingAddress, IEnumerable<Dto.PaymentMethod> PaymentMethods) : Message(CorrelationId: OrderId), ICommand;

    public record ProceedWithPayment(Guid PaymentId, Guid OrderId) : Message(CorrelationId: PaymentId), ICommand;

    public record CancelPayment(Guid PaymentId, Guid OrderId) : Message(CorrelationId: PaymentId), ICommand;

    public record AuthorizePaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: PaymentId), ICommand;

    public record DenyPaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: PaymentId), ICommand;

    public record CancelPaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: PaymentId), ICommand;

    public record RefundPaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: PaymentId), ICommand;

    public record DenyRefundPaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: PaymentId), ICommand;

    public record DenyCancelPaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: PaymentId), ICommand;
}