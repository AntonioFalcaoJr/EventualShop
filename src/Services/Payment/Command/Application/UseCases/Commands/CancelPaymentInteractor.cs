using Application.Abstractions;
using Application.Services;
using Contracts.Services.Payment;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class CancelPaymentInteractor : IInteractor<Command.CancelPayment>
{
    private readonly IApplicationService _applicationService;

    public CancelPaymentInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.CancelPayment command, CancellationToken cancellationToken)
    {
        var payment = await _applicationService.LoadAggregateAsync<Payment>(command.PaymentId, cancellationToken);
        payment.Handle(command);
        await _applicationService.AppendEventsAsync(payment, cancellationToken);
    }
}