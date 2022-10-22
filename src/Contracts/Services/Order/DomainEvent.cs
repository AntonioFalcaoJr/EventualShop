using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Order;

public static class DomainEvent
{
    public record OrderPlaced(Guid OrderId, Guid CustomerId, decimal Total, Dto.Address BillingAddress, Dto.Address ShippingAddress, IEnumerable<Dto.CartItem> Items, IEnumerable<Dto.PaymentMethod> PaymentMethods) : Message, IEvent;

    public record OrderConfirmed(Guid OrderId) : Message, IEvent;
}