using Application.Abstractions.EventSourcing.Projections;
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

    public Task Consume(ConsumeContext<DomainEvents.CartItemAdded> context)
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

        return _projectionsRepository.InsertAsync(shoppingCartItem, context.CancellationToken);
    }

    public Task Consume(ConsumeContext<DomainEvents.CartItemIncreased> context)
        => _projectionsRepository.IncreaseFieldAsync(
            id: context.Message.ItemId,
            field: item => item.Quantity,
            value: 1,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartItemDecreased> context)
        => _projectionsRepository.IncreaseFieldAsync(
            id: context.Message.ItemId,
            field: item => item.Quantity,
            value: -1,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartItemRemoved> context)
        => _projectionsRepository.DeleteAsync(context.Message.ItemId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartDiscarded> context)
        => _projectionsRepository.DeleteAsync(
            filter: item => item.ShoppingCartId == context.Message.CartId,
            cancellationToken: context.CancellationToken);
}