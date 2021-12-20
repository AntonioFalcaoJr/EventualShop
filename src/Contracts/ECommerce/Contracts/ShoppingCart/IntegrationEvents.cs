using ECommerce.Abstractions.Events;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.ShoppingCart;

public static class IntegrationEvents
{
    public record CartSubmitted(Guid CartId, Guid CustomerId, decimal Total, IEnumerable<Models.Item> CartItems, Models.Address BillingAddress, Models.Address ShippingAddress, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Event;
}