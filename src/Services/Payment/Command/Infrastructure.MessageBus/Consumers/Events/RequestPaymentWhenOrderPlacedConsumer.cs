using Application.UseCases.Events;
using Order = Contracts.Services.Order;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Events;

public class RequestPaymentWhenOrderPlacedConsumer : Consumer<Order.DomainEvent.OrderPlaced>
{
    public RequestPaymentWhenOrderPlacedConsumer(IRequestPaymentWhenOrderPlacedInteractor interactor)
        : base(interactor) { }
}