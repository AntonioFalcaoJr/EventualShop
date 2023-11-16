using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Shopping.Checkout;
using Domain.Aggregates.Checkouts;
using Domain.ValueObjects.PaymentMethods;

namespace Application.UseCases.Checkouts.Commands;

public class AddCreditCardInteractor(IApplicationService service) : IInteractor<Command.AddCreditCard>
{
    public async Task InteractAsync(Command.AddCreditCard cmd, CancellationToken cancellationToken)
    {
        var checkout = await service.LoadAggregateAsync<Checkout, CheckoutId>((CheckoutId)cmd.CheckoutId, cancellationToken);
        
        CreditCard creditCard = new(
            cmd.ExpirationDate,
            cmd.Number,
            cmd.HolderName,
            cmd.Cvv);

        checkout.AddCreditCard(creditCard);

        await service.AppendEventsAsync<Checkout, CheckoutId>(checkout, cancellationToken);
    }
}