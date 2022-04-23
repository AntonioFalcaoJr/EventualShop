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
    private readonly IProjectionsRepository<ECommerce.Contracts.ShoppingCarts.Projections.ShoppingCartItem> _projectionsRepository;

    public ProjectCartItemsWhenChangedConsumer(IProjectionsRepository<ECommerce.Contracts.ShoppingCarts.Projections.ShoppingCartItem> projectionsRepository)
    {
        _projectionsRepository = projectionsRepository;
    }

    public async Task Consume(ConsumeContext<DomainEvents.CartItemAdded> context)
    {
        var shoppingCartItem = new ECommerce.Contracts.ShoppingCarts.Projections.ShoppingCartItem(
            context.Message.CartId,
            context.Message.ProductId,
            context.Message.ProductName,
            context.Message.UnitPrice,
            context.Message.Quantity,
            context.Message.PictureUrl,
            context.Message.ItemId,
            false);

        await _projectionsRepository.InsertAsync(shoppingCartItem, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvents.CartItemIncreased> context)
        => await _projectionsRepository.IncreaseFieldAsync(
            id: context.Message.ItemId,
            field: item => item.Quantity,
            value: 1,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CartItemDecreased> context)
        => await _projectionsRepository.IncreaseFieldAsync(
            id: context.Message.ItemId,
            field: item => item.Quantity,
            value: -1,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CartItemRemoved> context)
        => await _projectionsRepository.DeleteAsync(context.Message.ItemId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CartDiscarded> context)
        => await _projectionsRepository.DeleteAsync(
            filter: item => item.ShoppingCartId == context.Message.CartId,
            cancellationToken: context.CancellationToken);
}