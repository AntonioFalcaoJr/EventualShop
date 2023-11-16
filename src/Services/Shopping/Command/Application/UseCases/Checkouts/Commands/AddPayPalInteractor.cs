using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Shopping.Checkout;
using Domain.Aggregates.Checkouts;
using Domain.ValueObjects.PaymentMethods;

namespace Application.UseCases.Checkouts.Commands;

public class AddPayPalInteractor(IApplicationService service) : IInteractor<Command.AddPayPal>
{
    public async Task InteractAsync(Command.AddPayPal cmd, CancellationToken cancellationToken)
    {
        var checkout = await service.LoadAggregateAsync<Checkout, CheckoutId>((CheckoutId)cmd.CheckoutId, cancellationToken);

        PayPal payPal = new(
            cmd.Email,
            cmd.Password);

        checkout.AddPayPal(payPal);

        await service.AppendEventsAsync<Checkout, CheckoutId>(checkout, cancellationToken);
    }
}