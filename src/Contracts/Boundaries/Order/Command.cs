using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Boundaries.Order;

public static class Command
{
    public record PlaceOrder(string CartId, string CustomerId, Dto.Money Total, Dto.Address BillingAddress, Dto.Address ShippingAddress, IEnumerable<Dto.CartItem> Items, IEnumerable<Dto.PaymentMethod> PaymentMethods) : Message, ICommand;

    public record ConfirmOrder(string OrderId) : Message, ICommand;

    public record CancelOrder(string OrderId) : Message, ICommand;
}