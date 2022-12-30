using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public interface IProjectBillingAddressListItemInteractor : IInteractor<DomainEvent.BillingAddressAdded> { }

public class ProjectBillingAddressListItemInteractor : IProjectBillingAddressListItemInteractor
{
    private readonly IProjectionGateway<Projection.BillingAddressListItem> _projectionGateway;

    public ProjectBillingAddressListItemInteractor(IProjectionGateway<Projection.BillingAddressListItem> projectionGateway)
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