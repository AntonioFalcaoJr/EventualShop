using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Shopping.Checkout;
using Domain.Aggregates.Checkouts;
using Domain.ValueObjects.PaymentMethods;

namespace Application.UseCases.Checkouts.Commands;

public class AddDebitCardInteractor(IApplicationService service) : IInteractor<Command.AddDebitCard>
{
    public async Task InteractAsync(Command.AddDebitCard cmd, CancellationToken cancellationToken)
    {
        var checkout = await service.LoadAggregateAsync<Checkout, CheckoutId>((CheckoutId)cmd.CheckoutId, cancellationToken);
        
        DebitCard debitCard = new(
            cmd.ExpirationDate,
            cmd.Number,
            cmd.HolderName,
            cmd.Cvv);

        checkout.AddDebitCard(debitCard);

        await service.AppendEventsAsync<Checkout, CheckoutId>(checkout, cancellationToken);
    }
}