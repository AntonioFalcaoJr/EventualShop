using Application.Abstractions;
using Contracts.Boundaries.Shopping.ShoppingCart;
using Contracts.DataTransferObjects;

namespace Application.UseCases.Events;

public interface IProjectCartDetailsWhenCartChangedInteractor :
    IInteractor<DomainEvent.ShoppingStarted>,
    IInteractor<DomainEvent.CartItemAdded>,
    IInteractor<DomainEvent.CartItemRemoved>,
    IInteractor<DomainEvent.CartCheckedOut>,
    IInteractor<DomainEvent.CartItemIncreased>,
    IInteractor<DomainEvent.CartItemDecreased>,
    IInteractor<DomainEvent.CartDiscarded>,
    IInteractor<SummaryEvent.CartProjectionRebuilt>;

public class ProjectCartDetailsWhenCartChangedInteractor(IProjectionGateway<Projection.ShoppingCartDetails> projectionGateway)
    : IProjectCartDetailsWhenCartChangedInteractor
{
    public async Task InteractAsync(DomainEvent.ShoppingStarted @event, CancellationToken cancellationToken)
    {
        Projection.ShoppingCartDetails shoppingCartDetails = new(
            new Guid(@event.CartId),
            new Guid(@event.CustomerId),
            new Dto.Money("0", "0"),
            @event.Status,
            false,
            Convert.ToUInt64(@event.Version));

        await projectionGateway.ReplaceInsertAsync(shoppingCartDetails, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.CartItemAdded @event, CancellationToken cancellationToken)
        => projectionGateway.UpdateFieldAsync(
            id: new Guid(@event.CartId),
            version: Convert.ToUInt64(@event.Version),
            field: cart => cart.Total,
            value: new Dto.Money("0", "0"),
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartItemRemoved @event, CancellationToken cancellationToken)
        => projectionGateway.UpdateFieldAsync(
            id: new Guid(@event.CartId),
            version: Convert.ToUInt64(@event.Version),
            field: cart => cart.Total,
            value: new Dto.Money("0", "0"),
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartItemIncreased @event, CancellationToken cancellationToken)
        => projectionGateway.UpdateFieldAsync(
            id: new Guid(@event.CartId),
            version: Convert.ToUInt64(@event.Version),
            field: cart => cart.Total,
            value: new Dto.Money("0", "0"),
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartItemDecreased @event, CancellationToken cancellationToken)
        => projectionGateway.UpdateFieldAsync(
            id: new Guid(@event.CartId),
            version: Convert.ToUInt64(@event.Version),
            field: cart => cart.Total,
            value: new Dto.Money("0", "0"),
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartCheckedOut @event, CancellationToken cancellationToken)
        => projectionGateway.UpdateFieldAsync(
            id: new Guid(@event.CartId),
            version: Convert.ToUInt64(@event.Version),
            field: cart => cart.Status,
            value: @event.Status,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartDiscarded @event, CancellationToken cancellationToken)
        => projectionGateway.DeleteAsync(new Guid(@event.CartId), cancellationToken);

    public async Task InteractAsync(SummaryEvent.CartProjectionRebuilt @event, CancellationToken cancellationToken)
    {
        Projection.ShoppingCartDetails shoppingCartDetails = new(
            new Guid(@event.Cart.Id),
            new Guid(@event.Cart.CustomerId),
            @event.Cart.Total,
            @event.Cart.Status,
            @event.Cart.IsDeleted,
            Convert.ToUInt64(@event.Version));

        await projectionGateway.RebuildInsertAsync(shoppingCartDetails, cancellationToken);
    }
}