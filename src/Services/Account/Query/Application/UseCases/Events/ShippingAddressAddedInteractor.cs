using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public class ShippingAddressAddedInteractor : IInteractor<DomainEvent.ShippingAddressAdded>
{
    private readonly IProjectionGateway<Projection.AddressListItem> _projectionGateway;

    public ShippingAddressAddedInteractor(IProjectionGateway<Projection.AddressListItem> projectionGateway)
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