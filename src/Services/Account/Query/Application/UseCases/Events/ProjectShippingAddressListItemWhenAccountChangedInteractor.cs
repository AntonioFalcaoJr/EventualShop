using Application.Abstractions;
using Contracts.Boundaries.Account;

namespace Application.UseCases.Events;

public interface IProjectShippingAddressListItemWhenAccountChangedInteractor :
    IInteractor<DomainEvent.ShippingAddressAdded>,
    IInteractor<DomainEvent.AccountDeleted>;

public class ProjectShippingAddressListItemWhenAccountChangedInteractor(IProjectionGateway<Projection.ShippingAddressListItem> projectionGateway)
    : IProjectShippingAddressListItemWhenAccountChangedInteractor
{
    public async Task InteractAsync(DomainEvent.ShippingAddressAdded @event, CancellationToken cancellationToken)
    {
        Projection.ShippingAddressListItem addressListItem =
            new(@event.AddressId,
                @event.AccountId,
                @event.Address,
                false,
                @event.Version);

        await projectionGateway.ReplaceInsertAsync(addressListItem, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.AccountDeleted @event, CancellationToken cancellationToken)
        => projectionGateway.DeleteAsync(@event.AccountId, cancellationToken);
}