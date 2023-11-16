using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Payment;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class CancelPaymentInteractor(IApplicationService service) : IInteractor<Command.CancelPayment>
{
    public async Task InteractAsync(Command.CancelPayment command, CancellationToken cancellationToken)
    {
        var payment = await service.LoadAggregateAsync<Payment>(command.PaymentId, cancellationToken);
        payment.Handle(command);
        await service.AppendEventsAsync(payment, cancellationToken);
    }
}