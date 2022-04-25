using Application.Abstractions.Projections;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectInventoryItemWhenChangedConsumer :
    IConsumer<DomainEvent.InventoryAdjustmentDecreased>,
    IConsumer<DomainEvent.InventoryAdjustmentIncreased>,
    IConsumer<DomainEvent.InventoryReceived>
{
    private readonly IProjectionRepository<Projection.Inventory> _repository;

    public ProjectInventoryItemWhenChangedConsumer(IProjectionRepository<Projection.Inventory> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEvent.InventoryAdjustmentDecreased> context)
        => await _repository.IncreaseFieldAsync(
            id: context.Message.ProductId,
            field: item => item.Quantity,
            value: context.Message.Quantity * -1,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.InventoryAdjustmentIncreased> context)
        => await _repository.IncreaseFieldAsync(
            id: context.Message.ProductId,
            field: item => item.Quantity,
            value: context.Message.Quantity,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.InventoryReceived> context)
    {
        var inventory = new Projection.Inventory
        {
            Id = context.Message.ProductId,
            Product = context.Message.Product,
            Quantity = context.Message.Quantity,
            IsDeleted = false
        };

        await _repository.InsertAsync(inventory, context.CancellationToken);
    }
}