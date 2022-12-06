using Application.Abstractions;
using Contracts.Services.Order;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class OrderConfirmedConsumer : Consumer<DomainEvent.OrderConfirmed>
{
    public OrderConfirmedConsumer(IInteractor<DomainEvent.OrderConfirmed> interactor)
        : base(interactor) { }
}