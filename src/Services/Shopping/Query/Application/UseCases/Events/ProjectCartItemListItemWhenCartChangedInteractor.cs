using Application.Abstractions;
using Contracts.Boundaries.Shopping.ShoppingCart;

namespace Application.UseCases.Events;

public interface IProjectCartItemListItemWhenCartChangedInteractor :
    IInteractor<DomainEvent.CartItemAdded>,
    IInteractor<DomainEvent.CartItemRemoved>,
    IInteractor<DomainEvent.CartItemIncreased>,
    IInteractor<DomainEvent.CartDiscarded>,
    IInteractor<DomainEvent.CartItemDecreased>;

public class ProjectCartItemListItemWhenCartChangedInteractor(IProjectionGateway<Projection.ShoppingCartItemListItem> projectionGateway)
    : IProjectCartItemListItemWhenCartChangedInteractor
{
    public async Task InteractAsync(DomainEvent.CartItemAdded @event, CancellationToken cancellationToken)
    {
        Projection.ShoppingCartItemListItem cartItemListItem = new(
            Guid.Parse(@event.ItemId),
            Guid.Parse(@event.CartId),
            @event.ProductName,
            Convert.ToInt32(@event.Quantity),
            false,
            10);

        await projectionGateway.ReplaceInsertAsync(cartItemListItem, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.CartItemIncreased @event, CancellationToken cancellationToken)
        => projectionGateway.UpdateFieldAsync(
            id: Guid.Parse(@event.ItemId),
            version: ulong.Parse(@event.Version),
            field: item => item.Quantity,
            value: int.Parse(@event.NewQuantity),
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartItemDecreased @event, CancellationToken cancellationToken)
        => projectionGateway.UpdateFieldAsync(
            id: Guid.Parse(@event.ItemId),
            version: ulong.Parse(@event.Version),
            field: item => item.Quantity,
            value: int.Parse(@event.NewQuantity),
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartItemRemoved @event, CancellationToken cancellationToken)
        => projectionGateway.DeleteAsync(Guid.Parse(@event.ItemId), cancellationToken);

    public Task InteractAsync(DomainEvent.CartDiscarded @event, CancellationToken cancellationToken)
        => projectionGateway.DeleteAsync(item => item.CartId == Guid.Parse(@event.CartId), cancellationToken);
}