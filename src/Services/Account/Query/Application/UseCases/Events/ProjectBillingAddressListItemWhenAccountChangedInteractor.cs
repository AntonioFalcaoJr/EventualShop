using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public interface IProjectBillingAddressListItemWhenAccountChangedInteractor :
    IInteractor<DomainEvent.BillingAddressAdded>,
    IInteractor<DomainEvent.AccountDeleted> { }

public class ProjectBillingAddressListItemWhenAccountChangedInteractor : IProjectBillingAddressListItemWhenAccountChangedInteractor
{
    private readonly IProjectionGateway<Projection.BillingAddressListItem> _projectionGateway;

    public ProjectBillingAddressListItemWhenAccountChangedInteractor(IProjectionGateway<Projection.BillingAddressListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.BillingAddressAdded @event, CancellationToken cancellationToken)
    {
        Projection.BillingAddressListItem addressListItem =
            new(@event.AddressId,
                @event.AccountId,
                @event.Address,
                false,
                @event.Version);

        await _projectionGateway.UpsertAsync(addressListItem, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.AccountDeleted @event, CancellationToken cancellationToken)
        => _projectionGateway.DeleteAsync(@event.AccountId, cancellationToken);
}