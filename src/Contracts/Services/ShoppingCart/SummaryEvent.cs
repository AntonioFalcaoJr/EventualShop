using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class SummaryEvent
{
    public record ProjectionRebuilt(Dto.ShoppingCart Cart) : Message, IEvent;
    
    public record CartSubmitted(Guid CartId, Guid CustomerId, Dto.Money Total, Dto.Address BillingAddress, Dto.Address ShippingAddress, IEnumerable<Dto.CartItem> Items, 
            IEnumerable<Dto.PaymentMethod> PaymentMethods) : Message, IEvent;
}