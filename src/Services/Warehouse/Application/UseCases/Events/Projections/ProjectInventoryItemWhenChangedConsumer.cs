using Application.Abstractions.Projections;
using Contracts.Services.Warehouse;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectInventoryItemWhenChangedConsumer :
    IConsumer<DomainEvent.InventoryAdjustmentDecreased>,
    IConsumer<DomainEvent.InventoryAdjustmentIncreased>,
    IConsumer<DomainEvent.InventoryReceived>
{
    private readonly IProjectionRepository<Projection.InventoryItem> _repository;

    public ProjectInventoryItemWhenChangedConsumer(IProjectionRepository<Projection.InventoryItem> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEvent.InventoryAdjustmentDecreased> context)
        => await _repository.IncreaseFieldAsync(
            id: context.Message.InventoryItemId,
            field: item => item.Quantity,
            value: context.Message.Quantity * -1,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.InventoryAdjustmentIncreased> context)
        => await _repository.IncreaseFieldAsync(
            id: context.Message.InventoryItemId,
            field: item => item.Quantity,
            value: context.Message.Quantity,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.InventoryReceived> context)
    {
        Projection.InventoryItem inventoryItem = new(
            context.Message.InventoryItemId,
            context.Message.InventoryId,
            context.Message.Product,
            context.Message.Quantity,
            false);

        await _repository.InsertAsync(inventoryItem, context.CancellationToken);
    }
}