using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Order;

public static class DomainEvent
{
    public record OrderPlaced(Guid OrderId, Dto.Customer Customer, IEnumerable<Dto.CartItem> Items, decimal Total, IEnumerable<Dto.IPaymentMethod> PaymentMethods) : Message(CorrelationId: OrderId), IEvent;

    public record OrderConfirmed(Guid OrderId) : Message(CorrelationId: OrderId), IEvent;
}