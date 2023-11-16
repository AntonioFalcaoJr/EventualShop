using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Shopping.Checkout;
using Domain.Aggregates.Checkouts;
using Domain.ValueObjects.Addresses;

namespace Application.UseCases.Checkouts.Commands;

public class AddBillingAddressInteractor(IApplicationService service) : IInteractor<Command.AddBillingAddress>
{
    public async Task InteractAsync(Command.AddBillingAddress cmd, CancellationToken cancellationToken)
    {
        var checkout = await service.LoadAggregateAsync<Checkout, CheckoutId>((CheckoutId)cmd.CheckoutId, cancellationToken);
        
        Address address = new(
            cmd.City,
            cmd.Complement,
            cmd.Country,
            cmd.Number,
            cmd.State,
            cmd.Street,
            cmd.ZipCode);

        checkout.AddBillingAddress(address);

        await service.AppendEventsAsync<Checkout, CheckoutId>(checkout, cancellationToken);
    }
}