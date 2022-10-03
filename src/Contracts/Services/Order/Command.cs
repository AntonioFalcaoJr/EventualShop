using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Order;

public static class Command
{
    public record PlaceOrder(Guid Id, Guid CartId, Guid CustomerId, decimal Total, Dto.Address BillingAddress, Dto.Address ShippingAddress, IEnumerable<Dto.CartItem> Items, IEnumerable<Dto.PaymentMethod> PaymentMethods) : Message(CorrelationId: Id), ICommand;

    public record ConfirmOrder(Guid Id) : Message(CorrelationId: Id), ICommand;

    public record CancelOrder(Guid Id) : Message(CorrelationId: Id), ICommand;
}