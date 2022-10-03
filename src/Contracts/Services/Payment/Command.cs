using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Payment;

public static class Command
{
    public record RequestPayment(Guid Id, decimal AmountDue, Dto.Address BillingAddress, IEnumerable<Dto.PaymentMethod> PaymentMethods) : Message(CorrelationId: Id), ICommand;

    public record ProceedWithPayment(Guid Id, Guid OrderId) : Message(CorrelationId: Id), ICommand;

    public record CancelPayment(Guid Id) : Message(CorrelationId: Id), ICommand;

    public record AuthorizePaymentMethod(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: Id), ICommand;

    public record DenyPaymentMethod(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: Id), ICommand;

    public record CancelPaymentMethod(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: Id), ICommand;

    public record RefundPaymentMethod(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: Id), ICommand;

    public record DenyPaymentMethodRefund(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: Id), ICommand;

    public record DenyPaymentMethodCancellation(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: Id), ICommand;
}