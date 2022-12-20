using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public class ShippingAddressAddedInteractor : 
    IInteractor<DomainEvent.ShippingAddressAdded>,
    IInteractor<IntegrationEvent.ProjectionRebuilt>
{
    private readonly IProjectionGateway<Projection.ShippingAddressListItem> _projectionGateway;

    public ShippingAddressAddedInteractor(IProjectionGateway<Projection.ShippingAddressListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.ShippingAddressAdded @event, CancellationToken cancellationToken)
    {
        Projection.ShippingAddressListItem addressListItem =
            new(@event.AddressId,
                @event.AccountId,
                @event.Address,
                false);

        await _projectionGateway.InsertAsync(addressListItem, cancellationToken);
    }

    public async Task InteractAsync(IntegrationEvent.ProjectionRebuilt @event, CancellationToken cancellationToken)
    {
        if (@event.Account.IsDeleted)
            return;

        foreach (var addressItem in @event.Account.Addresses)
        {
            Projection.ShippingAddressListItem addressListItem =
                new(addressItem.Id,
                    @event.AccountId,
                    addressItem.Address,
                    false);

            await _projectionGateway.ReplaceAsync(addressListItem, cancellationToken);
        }
    }
}