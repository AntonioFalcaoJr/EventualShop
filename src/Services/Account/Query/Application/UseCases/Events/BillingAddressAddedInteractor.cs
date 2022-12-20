using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public class BillingAddressAddedInteractor : 
    IInteractor<DomainEvent.BillingAddressAdded>,
    IInteractor<IntegrationEvent.ProjectionRebuilt>
{
    private readonly IProjectionGateway<Projection.BillingAddressListItem> _projectionGateway;

    public BillingAddressAddedInteractor(IProjectionGateway<Projection.BillingAddressListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.BillingAddressAdded @event, CancellationToken cancellationToken)
    {
        Projection.BillingAddressListItem addressListItem =
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
            Projection.BillingAddressListItem addressListItem =
                new(addressItem.Id,
                    @event.AccountId,
                    addressItem.Address,
                    false);

            await _projectionGateway.ReplaceAsync(addressListItem, cancellationToken);
        }
    }
}