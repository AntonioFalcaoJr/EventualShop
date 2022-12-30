using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public class BillingAddressRebuiltInteractor : IInteractor<IntegrationEvent.ProjectionRebuilt>
{
    private readonly IProjectionGateway<Projection.BillingAddressListItem> _projectionGateway;

    public BillingAddressRebuiltInteractor(IProjectionGateway<Projection.BillingAddressListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(IntegrationEvent.ProjectionRebuilt @event, CancellationToken cancellationToken)
    {
        foreach (var addressItem in @event.Account.Addresses)
        {
            Projection.BillingAddressListItem addressListItem =
                new(addressItem.Id,
                    @event.AccountId,
                    addressItem.Address,
                    false);

            await _projectionGateway.ReplaceAsync(addressListItem, cancellationToken);
        }
    }
}
