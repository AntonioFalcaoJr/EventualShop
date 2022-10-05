using Application.Abstractions.Projections;
using Contracts.Services.Warehouse;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectInventoryItemWhenChangedConsumer :
    IConsumer<DomainEvent.InventoryAdjustmentDecreased>,
    IConsumer<DomainEvent.InventoryAdjustmentIncreased>,
    IConsumer<DomainEvent.InventoryItemIncreased>,
    IConsumer<DomainEvent.InventoryItemReceived>
{
    private readonly IProjectionRepository<Projection.InventoryItem> _repository;

    public ProjectInventoryItemWhenChangedConsumer(IProjectionRepository<Projection.InventoryItem> repository)
        => _repository = repository;

    public Task Consume(ConsumeContext<DomainEvent.InventoryAdjustmentDecreased> context)
        => _repository.IncreaseFieldAsync(
            id: context.Message.ItemId,
            field: item => item.Quantity,
            value: context.Message.Quantity * -1,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.InventoryAdjustmentIncreased> context)
        => _repository.IncreaseFieldAsync(
            id: context.Message.ItemId,
            field: item => item.Quantity,
            value: context.Message.Quantity,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.InventoryItemIncreased> context)
        => _repository.IncreaseFieldAsync(
            id: context.Message.ItemId,
            field: item => item.Quantity,
            value: context.Message.Quantity,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.InventoryItemReceived> context)
    {
        Projection.InventoryItem inventoryItem = new(
            context.Message.ItemId,
            context.Message.Id,
            context.Message.Product,
            context.Message.Quantity,
            context.Message.Sku,
            false);

        return _repository.InsertAsync(inventoryItem, context.CancellationToken);
    }
}