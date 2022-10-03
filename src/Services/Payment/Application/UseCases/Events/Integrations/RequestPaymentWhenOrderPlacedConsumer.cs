using Application.EventStore;
using Contracts.Services.Payment;
using Domain.Aggregates;
using MassTransit;
using DomainEvent = Contracts.Services.Order.DomainEvent;

namespace Application.UseCases.Events.Integrations;

public class RequestPaymentWhenOrderPlacedConsumer : IConsumer<DomainEvent.OrderPlaced>
{
    private readonly IPaymentEventStoreService _eventStore;

    public RequestPaymentWhenOrderPlacedConsumer(IPaymentEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<DomainEvent.OrderPlaced> context)
    {
        Payment payment = new();

        payment.Handle(new Command.RequestPayment(
            context.Message.Id,
            context.Message.Total,
            context.Message.BillingAddress,
            context.Message.PaymentMethods));

        await _eventStore.AppendAsync(payment, context.CancellationToken);
    }
}