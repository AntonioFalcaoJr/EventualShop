using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public interface IProjectShippingAddressListItemWhenAccountChangedInteractor :
    IInteractor<DomainEvent.ShippingAddressAdded>,
    IInteractor<DomainEvent.AccountDeleted> { }

public class ProjectShippingAddressListItemWhenAccountChangedInteractor : IProjectShippingAddressListItemWhenAccountChangedInteractor
{
    private readonly IProjectionGateway<Projection.ShippingAddressListItem> _projectionGateway;

    public ProjectShippingAddressListItemWhenAccountChangedInteractor(IProjectionGateway<Projection.ShippingAddressListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.ShippingAddressAdded @event, CancellationToken cancellationToken)
    {
        Projection.ShippingAddressListItem addressListItem =
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