using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public class ProjectionRebuiltInteractor : IInteractor<IntegrationEvent.ProjectionRebuilt>
{
    private readonly IProjectionGateway<Projection.AccountDetails> _accountDetailsProjectionGateway;
    private readonly IProjectionGateway<Projection.BillingAddressListItem> _billingAddressListItemProjectionGateway;
    private readonly IProjectionGateway<Projection.ShippingAddressListItem> _shippingAddressListItemProjectionGateway;

    public ProjectionRebuiltInteractor(
        IProjectionGateway<Projection.AccountDetails> accountDetailsProjectionGateway,
        IProjectionGateway<Projection.BillingAddressListItem> billingAddressListItemProjectionGateway,
        IProjectionGateway<Projection.ShippingAddressListItem> shippingAddressListItemProjectionGateway)
    {
        _accountDetailsProjectionGateway = accountDetailsProjectionGateway;
        _billingAddressListItemProjectionGateway = billingAddressListItemProjectionGateway;
        _shippingAddressListItemProjectionGateway = shippingAddressListItemProjectionGateway;
    }

    public async Task InteractAsync(IntegrationEvent.ProjectionRebuilt @event, CancellationToken cancellationToken)
    {
        if (@event.IsDeleted)
            return;

        await RebuildAccountDetailsAsync(@event, cancellationToken);
        await RebuildBillingAddressListItemAsync(@event, cancellationToken);
        await RebuildShippingAddressListItemAsync(@event, cancellationToken);
    }

    private async Task RebuildAccountDetailsAsync(IntegrationEvent.ProjectionRebuilt @event, CancellationToken cancellationToken)
    {
        Projection.AccountDetails accountDetails =
            new(@event.AccountId,
                @event.Profile.FirstName,
                @event.Profile.LastName,
                @event.Profile.Email,
                false);

        await _accountDetailsProjectionGateway.ReplaceAsync(accountDetails, cancellationToken);
    }

    private async Task RebuildBillingAddressListItemAsync(IntegrationEvent.ProjectionRebuilt @event, CancellationToken cancellationToken)
    {
        foreach (var address in @event.Addresses)
        {
            Projection.BillingAddressListItem addressListItem =
                new(address.Key,
                    @event.AccountId,
                    address.Value,
                    false);
        
            await _billingAddressListItemProjectionGateway.ReplaceAsync(addressListItem, cancellationToken);
        }
    }

    private async Task RebuildShippingAddressListItemAsync(IntegrationEvent.ProjectionRebuilt @event, CancellationToken cancellationToken)
    {
        foreach (var address in @event.Addresses)
        {
            Projection.ShippingAddressListItem addressListItem =
                new(address.Key,
                    @event.AccountId,
                    address.Value,
                    false);

            await _shippingAddressListItemProjectionGateway.ReplaceAsync(addressListItem, cancellationToken);
        }
    }
}
