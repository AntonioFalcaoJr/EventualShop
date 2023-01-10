using Application.Abstractions;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Events;

public interface IProjectCartDetailsWhenCartChangedInteractor :
    IInteractor<DomainEvent.BillingAddressAdded>,
    IInteractor<DomainEvent.CartCreated>,
    IInteractor<DomainEvent.CartItemAdded>,
    IInteractor<DomainEvent.CartItemRemoved>,
    IInteractor<DomainEvent.CartCheckedOut>,
    IInteractor<DomainEvent.ShippingAddressAdded>,
    IInteractor<DomainEvent.CartItemIncreased>,
    IInteractor<DomainEvent.CartItemDecreased>,
    IInteractor<DomainEvent.CartDiscarded>,
    IInteractor<IntegrationEvent.ProjectionRebuilt> { }

public class ProjectCartDetailsWhenCartChangedInteractor : IProjectCartDetailsWhenCartChangedInteractor
{
    private readonly IProjectionGateway<Projection.ShoppingCartDetails> _projectionGateway;

    public ProjectCartDetailsWhenCartChangedInteractor(IProjectionGateway<Projection.ShoppingCartDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task InteractAsync(DomainEvent.CartCreated @event, CancellationToken cancellationToken)
    {
        Projection.ShoppingCartDetails shoppingCartDetails = new(
            @event.CartId,
            @event.CustomerId,
            null,
            null,
            @event.Status,
            default,
            false);

        return _projectionGateway.UpsertAsync(shoppingCartDetails, cancellationToken);
    }
    
    public Task InteractAsync(DomainEvent.CartItemAdded @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.CartId,
            field: cart => cart.Total,
            value: @event.NewCartTotal,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartItemRemoved @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.CartId,
            field: cart => cart.Total,
            value: @event.NewCartTotal,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.BillingAddressAdded @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.CartId,
            field: cart => cart.BillingAddress,
            value: @event.Address,
            cancellationToken: cancellationToken);
    
    public Task InteractAsync(DomainEvent.ShippingAddressAdded @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.CartId,
            field: cart => cart.ShippingAddress,
            value: @event.Address,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartItemIncreased @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.CartId,
            field: cart => cart.Total,
            value: @event.NewCartTotal,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartItemDecreased @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.CartId,
            field: cart => cart.Total,
            value: @event.NewCartTotal,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartCheckedOut @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.CartId,
            field: cart => cart.Status,
            value: @event.Status,
            cancellationToken: cancellationToken);
    
    public Task InteractAsync(DomainEvent.CartDiscarded @event, CancellationToken cancellationToken)
        => _projectionGateway.DeleteAsync(@event.CartId, cancellationToken);

    public Task InteractAsync(IntegrationEvent.ProjectionRebuilt @event, CancellationToken cancellationToken)
    {
        Projection.ShoppingCartDetails shoppingCartDetails = new(
            @event.Cart.Id,
            @event.Cart.CustomerId,
            @event.Cart.BillingAddress,
            @event.Cart.ShippingAddress,
            @event.Cart.Status,
            @event.Cart.Total,
            false);

        return _projectionGateway.UpsertAsync(shoppingCartDetails, cancellationToken);
    }
}