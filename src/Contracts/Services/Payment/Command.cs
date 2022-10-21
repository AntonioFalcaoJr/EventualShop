using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Payment;

public static class Command
{
    public record RequestPayment(Guid Id, Guid OrderId, decimal AmountDue, Dto.Address BillingAddress, IEnumerable<Dto.PaymentMethod> PaymentMethods) : Message, ICommand;

    public record ProceedWithPayment(Guid Id, Guid OrderId) : Message, ICommand;

    public record CancelPayment(Guid Id, Guid OrderId) : Message, ICommand;

    public record AuthorizePaymentMethod(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message, ICommand;

    public record DenyPaymentMethod(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message, ICommand;

    public record CancelPaymentMethod(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message, ICommand;

    public record RefundPaymentMethod(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message, ICommand;

    public record DenyPaymentMethodRefund(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message, ICommand;

    public record DenyPaymentMethodCancellation(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message, ICommand;
}