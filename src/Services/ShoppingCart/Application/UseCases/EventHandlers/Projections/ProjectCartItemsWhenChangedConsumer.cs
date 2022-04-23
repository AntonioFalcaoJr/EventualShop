using Application.Abstractions.Projections;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectCartItemsWhenChangedConsumer :
    IConsumer<DomainEvents.CartItemAdded>,
    IConsumer<DomainEvents.CartItemRemoved>,
    IConsumer<DomainEvents.CartItemIncreased>,
    IConsumer<DomainEvents.CartDiscarded>,
    IConsumer<DomainEvents.CartItemDecreased>
{
    private readonly IProjectionRepository<ECommerce.Contracts.ShoppingCarts.Projections.ShoppingCartItem> _projectionRepository;

    public ProjectCartItemsWhenChangedConsumer(IProjectionRepository<ECommerce.Contracts.ShoppingCarts.Projections.ShoppingCartItem> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<DomainEvents.CartItemAdded> context)
    {
        var shoppingCartItem = new ECommerce.Contracts.ShoppingCarts.Projections.ShoppingCartItem(
            context.Message.CartId,
            context.Message.Item.ProductId,
            context.Message.Item.ProductName,
            context.Message.Item.UnitPrice,
            context.Message.Item.Quantity,
            context.Message.Item.PictureUrl,
            context.Message.ItemId,
            false);

        await _projectionRepository.InsertAsync(shoppingCartItem, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvents.CartItemIncreased> context)
        => await _projectionRepository.IncreaseFieldAsync(
            id: context.Message.ItemId,
            field: item => item.Quantity,
            value: 1,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CartItemDecreased> context)
        => await _projectionRepository.IncreaseFieldAsync(
            id: context.Message.ItemId,
            field: item => item.Quantity,
            value: -1,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CartItemRemoved> context)
        => await _projectionRepository.DeleteAsync(context.Message.ItemId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CartDiscarded> context)
        => await _projectionRepository.DeleteAsync(
            filter: item => item.ShoppingCartId == context.Message.CartId,
            cancellationToken: context.CancellationToken);
}