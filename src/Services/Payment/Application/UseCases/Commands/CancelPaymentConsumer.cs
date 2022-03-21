using Application.EventSourcing.EventStore;
using MassTransit;
using CancelPaymentCommand = ECommerce.Contracts.Payment.Commands.CancelPayment;

namespace Application.UseCases.Commands;

public class CancelPaymentConsumer : IConsumer<CancelPaymentCommand>
{
    private readonly IPaymentEventStoreService _eventStoreService;

    public CancelPaymentConsumer(IPaymentEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<CancelPaymentCommand> context)
    {
        var payment = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.PaymentId, context.CancellationToken);
        payment.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(payment, context.CancellationToken);
    }
}