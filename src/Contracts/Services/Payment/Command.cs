using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Payment;

public static class Command
{
    public record RequestPayment(Guid OrderId, decimal AmountDue, Dto.Address BillingAddress, IEnumerable<Dto.PaymentMethod> PaymentMethods) : Message(CorrelationId: OrderId), ICommand;

    public record CancelPayment(Guid PaymentId, Guid OrderId) : Message(CorrelationId: PaymentId), ICommand;

    public record UpdatePaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId, bool Authorized) : Message(CorrelationId: PaymentId), ICommand;

    public record ProceedWithPayment(Guid PaymentId, Guid OrderId) : Message(CorrelationId: PaymentId), ICommand;
}