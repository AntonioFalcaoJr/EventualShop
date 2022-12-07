using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Payment;

public static class Command
{
    public record RequestPayment(Guid OrderId, decimal AmountDue, Dto.Address BillingAddress, IEnumerable<Dto.PaymentMethod> PaymentMethods) : Message, ICommand;

    public record ProceedWithPayment(Guid PaymentId, Guid OrderId) : Message, ICommand;

    public record CancelPayment(Guid PaymentId, Guid OrderId) : Message, ICommand;

    public record AuthorizePaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message, ICommand;

    public record DenyPaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message, ICommand;

    public record CancelPaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message, ICommand;

    public record RefundPaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message, ICommand;

    public record DenyPaymentMethodRefund(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message, ICommand;

    public record DenyPaymentMethodCancellation(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message, ICommand;
}