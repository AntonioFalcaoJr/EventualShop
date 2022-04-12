using Application.EventSourcing.EventStore;
using ECommerce.Contracts.Payments;
using MassTransit;
using Commands = ECommerce.Contracts.Orders.Commands;

namespace Application.UseCases.EventHandlers.Integrations;

public class ConfirmOrderWhenPaymentCompletedConsumer : IConsumer<DomainEvents.PaymentCompleted>
{
    private readonly IOrderEventStoreService _eventStoreService;

    public ConfirmOrderWhenPaymentCompletedConsumer(IOrderEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DomainEvents.PaymentCompleted> context)
    {
        var order = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.OrderId, context.CancellationToken);
        order.Handle(new Commands.ConfirmOrder(context.Message.OrderId));
        await _eventStoreService.AppendEventsToStreamAsync(order, context.CancellationToken);
    }
}