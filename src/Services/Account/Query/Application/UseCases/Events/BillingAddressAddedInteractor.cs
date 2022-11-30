using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public class BillingAddressAddedInteractor : IInteractor<DomainEvent.BillingAddressAdded>
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
}