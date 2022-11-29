using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public class BillingAddressAddedInteractor : IInteractor<DomainEvent.BillingAddressAdded>
{
    private readonly IProjectionGateway<Projection.AddressListItem> _projectionGateway;

    public BillingAddressAddedInteractor(IProjectionGateway<Projection.AddressListItem> projectionGateway)
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
}