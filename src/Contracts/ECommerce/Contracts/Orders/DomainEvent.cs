using ECommerce.Abstractions;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Orders;

public static class DomainEvent
{
    public record OrderPlaced(Guid OrderId, Models.Customer Customer, IEnumerable<Models.ShoppingCartItem> Items, decimal Total, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Message(CorrelationId: OrderId), IEvent;

    public record OrderConfirmed(Guid OrderId) : Message(CorrelationId: OrderId), IEvent;
}