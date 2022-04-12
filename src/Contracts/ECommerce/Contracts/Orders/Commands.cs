using ECommerce.Abstractions.Messages.Commands;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Orders;

public static class Commands
{
    public record PlaceOrder(Models.Customer Customer, IEnumerable<Models.ShoppingCartItem> Items, decimal Total, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Command(CorrelationId: Customer.Id);

    public record ConfirmOrder(Guid OrderId) : Command(CorrelationId: OrderId);
}