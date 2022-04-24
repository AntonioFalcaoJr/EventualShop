using Application.EventStore;
using ECommerce.Contracts.Payments;
using MassTransit;

namespace Application.UseCases.Commands;

public class CancelPaymentConsumer : IConsumer<Command.CancelPayment>
{
    private readonly IPaymentEventStoreService _eventStore;

    public CancelPaymentConsumer(IPaymentEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.CancelPayment> context)
    {
        var payment = await _eventStore.LoadAggregateAsync(context.Message.PaymentId, context.CancellationToken);
        payment.Handle(context.Message);
        await _eventStore.AppendEventsAsync(payment, context.CancellationToken);
    }
}