using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Order;

public static class DomainEvent
{
    public record OrderPlaced(Guid Id, Guid CustomerId, decimal Total, Dto.Address BillingAddress, Dto.Address ShippingAddress, IEnumerable<Dto.CartItem> Items, IEnumerable<Dto.PaymentMethod> PaymentMethods) : Message(CorrelationId: Id), IEvent;

    public record OrderConfirmed(Guid Id) : Message(CorrelationId: Id), IEvent;
}