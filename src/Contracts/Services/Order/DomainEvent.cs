using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Order;

public static class DomainEvent
{
    public record OrderPlaced(Guid OrderId, Guid CustomerId, string Total, Dto.Address BillingAddress, Dto.Address ShippingAddress, IEnumerable<Dto.OrderItem> Items, 
        IEnumerable<Dto.PaymentMethod> PaymentMethods, string Status) : Message, IEvent;

    public record OrderConfirmed(Guid OrderId, string Status) : Message, IEvent;
}