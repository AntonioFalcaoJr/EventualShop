using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Orders;

public static class DomainEvent
{
    public record OrderPlaced(Guid OrderId, Dto.Customer Customer, IEnumerable<Dto.ShoppingCartItem> Items, decimal Total, IEnumerable<Dto.IPaymentMethod> PaymentMethods) : Message(CorrelationId: OrderId), IEvent;

    public record OrderConfirmed(Guid OrderId) : Message(CorrelationId: OrderId), IEvent;
}