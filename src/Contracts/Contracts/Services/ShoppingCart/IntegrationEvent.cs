using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class IntegrationEvent
{
    public record CartSubmitted(Guid CartId, Dto.Customer Customer, decimal Total, IEnumerable<Dto.CartItem> ShoppingCartItems, IEnumerable<Dto.IPaymentMethod> PaymentMethods) : Message(CorrelationId: CartId), IEvent;
}