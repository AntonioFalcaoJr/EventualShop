using Application.Abstractions;
using Application.Services;
using Domain.Aggregates;
using Contracts.Services.Order;
using Payment = Contracts.Services.Payment;

namespace Application.UseCases.Events;

public interface IConfirmOrderWhenPaymentCompletedInteractor : IInteractor<Payment.DomainEvent.PaymentCompleted> { }

public class ConfirmOrderWhenPaymentCompletedInteractor : IConfirmOrderWhenPaymentCompletedInteractor
{
    private readonly IApplicationService _applicationService;

    public ConfirmOrderWhenPaymentCompletedInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Payment.DomainEvent.PaymentCompleted @event, CancellationToken cancellationToken)
    {
        var order = await _applicationService.LoadAggregateAsync<Order>(@event.OrderId, cancellationToken);
        order.Handle(new Command.ConfirmOrder(@event.OrderId));
        await _applicationService.AppendEventsAsync(order, cancellationToken);
    }
}