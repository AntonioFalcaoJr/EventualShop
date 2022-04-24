using Application.Abstractions.Projections;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectInventoryItemWhenChangedConsumer :
    IConsumer<DomainEvents.InventoryAdjusted>,
    IConsumer<DomainEvents.InventoryReceived>
{
    private readonly IProjectionRepository<ECommerce.Contracts.Warehouses.Projection.Inventory> _projectionRepository;

    public ProjectInventoryItemWhenChangedConsumer(IProjectionRepository<ECommerce.Contracts.Warehouses.Projection.Inventory> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    // TODO - It should be some think like (InventoryAdjustmentIncreased and InventoryAdjustmentDecreased)
    public async Task Consume(ConsumeContext<DomainEvents.InventoryAdjusted> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.ProductId,
            field: item => item.Quantity,
            value: context.Message.Quantity,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.InventoryReceived> context)
    {
        var inventory = new ECommerce.Contracts.Warehouses.Projection.Inventory
        {
            Id = context.Message.ProductId,
            Description = context.Message.Description,
            Name = context.Message.Name,
            Quantity = context.Message.Quantity,
            Sku = context.Message.Sku,
            IsDeleted = false
        };

        await _projectionRepository.InsertAsync(inventory, context.CancellationToken);
    }
}