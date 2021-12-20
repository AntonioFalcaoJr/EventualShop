using ECommerce.Abstractions.Commands;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Order;

public static class Commands
{
    public record PlaceOrder(Guid CustomerId, IEnumerable<Models.Item> Items, decimal Total, Models.Address BillingAddress, Models.Address ShippingAddress, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Command;

    public record ConfirmOrder(Guid OrderId) : Command;
}