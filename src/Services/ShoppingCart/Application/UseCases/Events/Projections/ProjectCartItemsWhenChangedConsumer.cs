using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using ECommerce.Contracts.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectCartItemsWhenChangedConsumer :
    IConsumer<DomainEvents.CartItemAdded>,
    IConsumer<DomainEvents.CartItemRemoved>,
    IConsumer<DomainEvents.CartItemIncreased>,
    IConsumer<DomainEvents.CartItemDecreased>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;
    private readonly IShoppingCartProjectionsService _projectionsService;

    public ProjectCartItemsWhenChangedConsumer(
        IShoppingCartEventStoreService eventStoreService,
        IShoppingCartProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<DomainEvents.CartItemAdded> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartItemIncreased> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartItemDecreased> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartItemRemoved> context)
        => _projectionsService.RemoveAsync<ShoppingCartItemProjection>(item 
            => item.Id == context.Message.ItemId, context.CancellationToken);

    private async Task ProjectAsync(Guid cartId, CancellationToken cancellationToken)
    {
        var cart = await _eventStoreService.LoadAggregateFromStreamAsync(cartId, cancellationToken);

        var shoppingCartItemProjections = cart.Items.Select(item
            => new ShoppingCartItemProjection
            {
                Id = item.Id,
                CartId = cart.Id,
                Quantity = item.Quantity,
                PictureUrl = item.PictureUrl,
                ProductName = item.ProductName,
                UnitPrice = item.UnitPrice,
                ProductId = item.ProductId,
                IsDeleted = item.IsDeleted
            }
        );

        await _projectionsService.ProjectManyAsync(shoppingCartItemProjections, cancellationToken);
    }
}