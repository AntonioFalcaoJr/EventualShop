using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Order;

public static class DomainEvents
{
    public record OrderPlaced(Guid OrderId, Models.Customer Customer, IEnumerable<Models.ShoppingCartItem> Items, decimal Total, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Event(CorrelationId: OrderId);

    public record OrderConfirmed(Guid OrderId) : Event(CorrelationId: OrderId);
}