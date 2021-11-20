using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.Services;
using Domain.Aggregates;
using MassTransit;
using ProceedWithPaymentCommand = Messages.Services.Payments.Commands.ProceedWithPayment;

namespace Application.UseCases.Commands;

public class ProceedWithPaymentConsumer : IConsumer<ProceedWithPaymentCommand>
{
    private readonly IPaymentEventStoreService _eventStoreService;
    private readonly IPaymentStrategy _strategy;

    public ProceedWithPaymentConsumer(IPaymentEventStoreService eventStoreService, IPaymentStrategy strategy)
    {
        _eventStoreService = eventStoreService;
        _strategy = strategy;
    }

    public async Task Consume(ConsumeContext<ProceedWithPaymentCommand> context)
    {
        // TODO - Revisar necessidade
        
        var payment = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.PaymentId, context.CancellationToken);
        await _strategy.ProceedWithPaymentAsync(payment, context.CancellationToken);
        payment.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(payment, context.CancellationToken);
    }
}