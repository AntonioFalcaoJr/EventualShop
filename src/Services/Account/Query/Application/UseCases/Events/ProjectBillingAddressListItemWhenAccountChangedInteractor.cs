using Application.Abstractions;
using Contracts.Boundaries.Account;

namespace Application.UseCases.Events;

public interface IProjectBillingAddressListItemWhenAccountChangedInteractor :
    IInteractor<DomainEvent.BillingAddressAdded>,
    IInteractor<DomainEvent.AccountDeleted>;

public class ProjectBillingAddressListItemWhenAccountChangedInteractor(IProjectionGateway<Projection.BillingAddressListItem> projectionGateway)
    : IProjectBillingAddressListItemWhenAccountChangedInteractor
{
    public async Task InteractAsync(DomainEvent.BillingAddressAdded @event, CancellationToken cancellationToken)
    {
        Projection.BillingAddressListItem addressListItem =
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