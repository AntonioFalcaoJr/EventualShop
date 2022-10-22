using Application.EventStore;
using Contracts.Services.Payment;
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
        var payment = await _eventStore.LoadAsync(context.Message.PaymentId, context.CancellationToken);
        payment.Handle(context.Message);
        await _eventStore.AppendAsync(payment, context.CancellationToken);
    }
}