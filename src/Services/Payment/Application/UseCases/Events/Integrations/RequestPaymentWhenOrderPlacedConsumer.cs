using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using OrderPlacedEvent = Messages.Services.Orders.DomainEvents.OrderPlaced;
using RequestPaymentCommand = Messages.Services.Payments.Commands.RequestPayment;

namespace Application.UseCases.Events.Integrations;

public class RequestPaymentWhenOrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly IPaymentEventStoreService _eventStoreService;

    public RequestPaymentWhenOrderPlacedConsumer(IPaymentEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        var payment = new Payment();

        payment.Handle(new RequestPaymentCommand(
            context.Message.OrderId,
            0,
            context.Message.BillingAddress,
            context.Message.CreditCard));

        await _eventStoreService.AppendEventsToStreamAsync(payment, context.CancellationToken);
    }
}