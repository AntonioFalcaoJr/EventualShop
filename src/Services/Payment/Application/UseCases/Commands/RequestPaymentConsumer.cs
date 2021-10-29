using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using RequestPaymentCommand = Messages.Payments.Commands.RequestPayment;

namespace Application.UseCases.Commands;

public class RequestPaymentConsumer : IConsumer<RequestPaymentCommand>
{
    private readonly IPaymentEventStoreService _eventStoreService;

    public RequestPaymentConsumer(IPaymentEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<RequestPaymentCommand> context)
    {
        var payment = new Payment();
        payment.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(payment, context.CancellationToken);
    }
}