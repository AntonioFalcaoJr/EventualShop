using Application.Abstractions.Projections;
using Contracts.Services.ShoppingCart;
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
        => _repository = repository;

    public Task Consume(ConsumeContext<DomainEvent.CartItemAdded> context)
    {
        Projection.ShoppingCartItem item = new(
            context.Message.Id,
            context.Message.ItemId,
            context.Message.Product,
            context.Message.Quantity,
            false);

        return _repository.InsertAsync(item, context.CancellationToken);
    }

    public Task Consume(ConsumeContext<DomainEvent.CartItemIncreased> context)
        => _repository.IncreaseFieldAsync(
            id: context.Message.ItemId,
            field: item => item.Quantity,
            value: 1,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemDecreased> context)
        => _repository.IncreaseFieldAsync(
            id: context.Message.ItemId,
            field: item => item.Quantity,
            value: -1,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemRemoved> context)
        => _repository.DeleteAsync(context.Message.ItemId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartDiscarded> context)
        => _repository.DeleteAsync(
            filter: item => item.Id == context.Message.Id,
            cancellationToken: context.CancellationToken);
}