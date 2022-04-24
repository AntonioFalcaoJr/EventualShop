using Application.EventStore;
using ECommerce.Contracts.Payments;
using MassTransit;

namespace Application.UseCases.Commands;

public class CancelPaymentConsumer : IConsumer<Command.CancelPayment>
{
    private readonly IPaymentEventStoreService _eventStoreService;

    public CancelPaymentConsumer(IPaymentEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.CancelPayment> context)
    {
        var payment = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.PaymentId, context.CancellationToken);
        payment.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(payment, context.CancellationToken);
    }
}