using Application.Abstractions;
using Contracts.Services.Order;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class OrderPlacedConsumer : Consumer<DomainEvent.OrderPlaced>
{
    public OrderPlacedConsumer(IInteractor<DomainEvent.OrderPlaced> interactor)
        : base(interactor) { }
}