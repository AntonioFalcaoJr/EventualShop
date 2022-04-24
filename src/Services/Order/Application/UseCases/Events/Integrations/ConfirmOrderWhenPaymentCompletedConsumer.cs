using Application.EventStore;
using ECommerce.Contracts.Payments;
using MassTransit;
using Command = ECommerce.Contracts.Orders.Command;

namespace Application.UseCases.Events.Integrations;

public class ConfirmOrderWhenPaymentCompletedConsumer : IConsumer<DomainEvent.PaymentCompleted>
{
    private readonly IOrderEventStoreService _eventStore;

    public ConfirmOrderWhenPaymentCompletedConsumer(IOrderEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<DomainEvent.PaymentCompleted> context)
    {
        var order = await _eventStore.LoadAggregateAsync(context.Message.OrderId, context.CancellationToken);
        order.Handle(new Command.ConfirmOrder(context.Message.OrderId));
        await _eventStore.AppendEventsAsync(order, context.CancellationToken);
    }
}