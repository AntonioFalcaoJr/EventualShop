using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public class ShippingAddressRebuiltInteractor : IInteractor<IntegrationEvent.ProjectionRebuilt>
{
    private readonly IProjectionGateway<Projection.ShippingAddressListItem> _projectionGateway;

    public ShippingAddressRebuiltInteractor(IProjectionGateway<Projection.ShippingAddressListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(IntegrationEvent.ProjectionRebuilt @event, CancellationToken cancellationToken)
    {
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
