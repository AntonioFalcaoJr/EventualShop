using ECommerce.Abstractions;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Orders;

public static class Command
{
    public record PlaceOrder(Models.Customer Customer, IEnumerable<Models.ShoppingCartItem> Items, decimal Total, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Message(CorrelationId: Customer.Id), ICommand;

    public record ConfirmOrder(Guid OrderId) : Message(CorrelationId: OrderId), ICommand;
    
    public record CancelOrder(Guid OrderId) : Message(CorrelationId: OrderId), ICommand;
}