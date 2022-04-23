using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.ShoppingCarts;

public static class IntegrationEvents
{
    public record CartSubmitted(Guid ShoppingCartId, Models.Customer Customer, decimal Total, IEnumerable<Models.ShoppingCartItem> ShoppingCartItems, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Event(CorrelationId: ShoppingCartId);
}