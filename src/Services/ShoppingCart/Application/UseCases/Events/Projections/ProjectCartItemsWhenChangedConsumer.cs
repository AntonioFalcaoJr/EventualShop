using Application.Abstractions.Projections;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectCartItemsWhenChangedConsumer :
    IConsumer<DomainEvent.CartItemAdded>,
    IConsumer<DomainEvent.CartItemRemoved>,
    IConsumer<DomainEvent.CartItemIncreased>,
    IConsumer<DomainEvent.CartDiscarded>,
    IConsumer<DomainEvent.CartItemDecreased>
{
    private readonly IProjectionRepository<Projection.ShoppingCartItem> _repository;

    public ProjectCartItemsWhenChangedConsumer(IProjectionRepository<Projection.ShoppingCartItem> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEvent.CartItemAdded> context)
    {
        Projection.ShoppingCartItem item = new(
            context.Message.CartId,
            context.Message.ItemId,
            context.Message.Product,
            context.Message.Quantity,
            false);

        await _repository.InsertAsync(item, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvent.CartItemIncreased> context)
        => await _repository.IncreaseFieldAsync(
            id: context.Message.ItemId,
            field: item => item.Quantity,
            value: 1,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CartItemDecreased> context)
        => await _repository.IncreaseFieldAsync(
            id: context.Message.ItemId,
            field: item => item.Quantity,
            value: -1,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CartItemRemoved> context)
        => await _repository.DeleteAsync(context.Message.ItemId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CartDiscarded> context)
        => await _repository.DeleteAsync(
            filter: item => item.CartId == context.Message.CartId,
            cancellationToken: context.CancellationToken);
}