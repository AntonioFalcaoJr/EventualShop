using Application.EventStore;
using ECommerce.Contracts.Payments;
using MassTransit;
using Command = ECommerce.Contracts.Orders.Command;

namespace Application.UseCases.EventHandlers.Integrations;

public class ConfirmOrderWhenPaymentCompletedConsumer : IConsumer<DomainEvent.PaymentCompleted>
{
    private readonly IOrderEventStoreService _eventStoreService;

    public ConfirmOrderWhenPaymentCompletedConsumer(IOrderEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DomainEvent.PaymentCompleted> context)
    {
        var order = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.OrderId, context.CancellationToken);
        order.Handle(new Command.ConfirmOrder(context.Message.OrderId));
        await _eventStoreService.AppendEventsToStreamAsync(order, context.CancellationToken);
    }
}