using Contracts.Abstractions.Messages;
using Contracts.Services.Order;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Orders.Validators;

namespace WebAPI.APIs.Orders;

public static class Requests
{
    public record CancelOrder(IBus Bus, Guid OrderId, CancellationToken CancellationToken)
        : Validatable<CancelOrderValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.CancelOrder(OrderId);
    }
}