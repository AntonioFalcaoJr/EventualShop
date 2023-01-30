using Application.Abstractions;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Events;

public interface IProjectCartItemListItemWhenCartChangedInteractor :
    IInteractor<DomainEvent.CartItemAdded>,
    IInteractor<DomainEvent.CartItemRemoved>,
    IInteractor<DomainEvent.CartItemIncreased>,
    IInteractor<DomainEvent.CartDiscarded>,
    IInteractor<DomainEvent.CartItemDecreased> { }

public class ProjectCartItemListItemWhenCartChangedInteractor : IProjectCartItemListItemWhenCartChangedInteractor
{
    private readonly IProjectionGateway<Projection.ShoppingCartItemListItem> _projectionGateway;

    public ProjectCartItemListItemWhenCartChangedInteractor(IProjectionGateway<Projection.ShoppingCartItemListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task InteractAsync(DomainEvent.CartItemAdded @event, CancellationToken cancellationToken)
    {
        Projection.ShoppingCartItemListItem cartItemListItem = new(
            @event.ItemId,
            @event.CartId,
            @event.Product.Name,
            @event.Quantity,
            false,
            @event.Version);

        return _projectionGateway.UpsertAsync(cartItemListItem, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.CartItemIncreased @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.ItemId,
            version: @event.Version,
            field: item => item.Quantity,
            value: @event.NewQuantity,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartItemDecreased @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.ItemId,
            version: @event.Version,
            field: item => item.Quantity,
            value: @event.NewQuantity,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartItemRemoved @event, CancellationToken cancellationToken)
        => _projectionGateway.DeleteAsync(@event.ItemId, cancellationToken);

    public Task InteractAsync(DomainEvent.CartDiscarded @event, CancellationToken cancellationToken)
        => _projectionGateway.DeleteAsync(item => item.CartId == @event.CartId, cancellationToken);
}