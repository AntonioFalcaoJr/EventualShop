using Application.EventSourcing.EventStore;
using ECommerce.Contracts.Payments;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class CancelPaymentConsumer : IConsumer<Commands.CancelPayment>
{
    private readonly IPaymentEventStoreService _eventStoreService;

    public CancelPaymentConsumer(IPaymentEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.CancelPayment> context)
    {
        var payment = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.PaymentId, context.CancellationToken);
        payment.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(payment, context.CancellationToken);
    }
}