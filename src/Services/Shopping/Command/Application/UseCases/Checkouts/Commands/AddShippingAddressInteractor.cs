using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Shopping.Checkout;
using Domain.Aggregates.Checkouts;
using Domain.ValueObjects.Addresses;

namespace Application.UseCases.Checkouts.Commands;

public class AddShippingAddressInteractor(IApplicationService service) : IInteractor<Command.AddShippingAddress>
{
    public async Task InteractAsync(Command.AddShippingAddress cmd, CancellationToken cancellationToken)
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

        checkout.AddShippingAddress(address);

        await service.AppendEventsAsync<Checkout, CheckoutId>(checkout, cancellationToken);
    }
}