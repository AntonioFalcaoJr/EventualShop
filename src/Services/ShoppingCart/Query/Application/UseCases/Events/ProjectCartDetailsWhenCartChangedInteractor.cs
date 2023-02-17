using Application.Abstractions;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Events;

public interface IProjectCartDetailsWhenCartChangedInteractor :
    IInteractor<DomainEvent.CartCreated>,
    IInteractor<DomainEvent.CartItemAdded>,
    IInteractor<DomainEvent.CartItemRemoved>,
    IInteractor<DomainEvent.CartCheckedOut>,
    IInteractor<DomainEvent.CartItemIncreased>,
    IInteractor<DomainEvent.CartItemDecreased>,
    IInteractor<DomainEvent.CartDiscarded>,
    IInteractor<SummaryEvent.CartProjectionRebuilt> { }

public class ProjectCartDetailsWhenCartChangedInteractor : IProjectCartDetailsWhenCartChangedInteractor
{
    private readonly IProjectionGateway<Projection.ShoppingCartDetails> _projectionGateway;

    public ProjectCartDetailsWhenCartChangedInteractor(IProjectionGateway<Projection.ShoppingCartDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CartCreated @event, CancellationToken cancellationToken)
    {
        Projection.ShoppingCartDetails shoppingCartDetails = new(
            @event.CartId,
            @event.CustomerId,
            @event.Total,
            @event.Status,
            false,
            @event.Version);

        await _projectionGateway.ReplaceInsertAsync(shoppingCartDetails, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.CartItemAdded @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.CartId,
            version: @event.Version,
            field: cart => cart.Total,
            value: @event.NewCartTotal,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartItemRemoved @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.CartId,
            version: @event.Version,
            field: cart => cart.Total,
            value: @event.NewCartTotal,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartItemIncreased @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.CartId,
            version: @event.Version,
            field: cart => cart.Total,
            value: @event.NewCartTotal,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartItemDecreased @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.CartId,
            version: @event.Version,
            field: cart => cart.Total,
            value: @event.NewCartTotal,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartCheckedOut @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.CartId,
            version: @event.Version,
            field: cart => cart.Status,
            value: @event.Status,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.CartDiscarded @event, CancellationToken cancellationToken)
        => _projectionGateway.DeleteAsync(@event.CartId, cancellationToken);

    public async Task InteractAsync(SummaryEvent.CartProjectionRebuilt @event, CancellationToken cancellationToken)
    {
        Projection.ShoppingCartDetails shoppingCartDetails = new(
            @event.Cart.Id,
            @event.Cart.CustomerId,
            @event.Cart.Total,
            @event.Cart.Status,
            false,
            @event.Version);

        await _projectionGateway.ReplaceInsertAsync(shoppingCartDetails, cancellationToken);
    }
}