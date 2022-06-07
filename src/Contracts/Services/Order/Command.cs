using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Order;

public static class Command
{
    public record PlaceOrder(Guid CartId, Guid CustomerId, decimal Total, Dto.Address BillingAddress, Dto.Address ShippingAddress, IEnumerable<Dto.CartItem> Items, IEnumerable<Dto.PaymentMethod> PaymentMethods)
        : Message(CorrelationId: CustomerId), ICommand;

    public record ConfirmOrder(Guid OrderId) : Message(CorrelationId: OrderId), ICommand;

    public record CancelOrder(Guid OrderId) : Message(CorrelationId: OrderId), ICommand;
}