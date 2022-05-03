using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Order;

public static class Command
{
    public record PlaceOrder(Dto.Customer Customer, IEnumerable<Dto.CartItem> Items, decimal Total, IEnumerable<Dto.IPaymentMethod> PaymentMethods) : Message(CorrelationId: Customer.Id), ICommand;

    public record ConfirmOrder(Guid OrderId) : Message(CorrelationId: OrderId), ICommand;
    
    public record CancelOrder(Guid OrderId) : Message(CorrelationId: OrderId), ICommand;
}