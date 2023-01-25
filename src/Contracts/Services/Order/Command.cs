using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Order;

public static class Command
{
    public record PlaceOrder(Guid CartId, Guid CustomerId, Dto.Money Total, Dto.Address BillingAddress, Dto.Address ShippingAddress, IEnumerable<Dto.CartItem> Items, IEnumerable<Dto.PaymentMethod> PaymentMethods) : Message, ICommand;

    public record ConfirmOrder(Guid OrderId) : Message, ICommand;

    public record CancelOrder(Guid OrderId) : Message, ICommand;
}