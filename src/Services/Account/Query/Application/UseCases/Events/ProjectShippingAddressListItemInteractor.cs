using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public interface IProjectShippingAddressListItemInteractor : IInteractor<DomainEvent.ShippingAddressAdded> { }

public class ProjectShippingAddressListItemInteractor : IProjectShippingAddressListItemInteractor
{
    private readonly IProjectionGateway<Projection.ShippingAddressListItem> _projectionGateway;

    public ProjectShippingAddressListItemInteractor(IProjectionGateway<Projection.ShippingAddressListItem> projectionGateway)
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
}