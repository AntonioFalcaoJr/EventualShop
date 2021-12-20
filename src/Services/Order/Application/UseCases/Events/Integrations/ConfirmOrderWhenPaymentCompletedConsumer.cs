using Application.EventSourcing.EventStore;
using MassTransit;
using PaymentCompletedEvent = ECommerce.Contracts.Payment.DomainEvents.PaymentCompleted;
using ConfirmOrderCommand = ECommerce.Contracts.Order.Commands.ConfirmOrder;

namespace Application.UseCases.Events.Integrations;

public class ConfirmOrderWhenPaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
{
    private readonly IOrderEventStoreService _eventStoreService;

    public ConfirmOrderWhenPaymentCompletedConsumer(IOrderEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        var order = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.OrderId, context.CancellationToken);
        order.Handle(new ConfirmOrderCommand(context.Message.OrderId));
        await _eventStoreService.AppendEventsToStreamAsync(order, context.CancellationToken);
    }
}