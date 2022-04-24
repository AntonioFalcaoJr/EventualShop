using Application.Abstractions.Projections;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectInventoryItemWhenChangedConsumer :
    IConsumer<DomainEvent.InventoryAdjusted>,
    IConsumer<DomainEvent.InventoryReceived>
{
    private readonly IProjectionRepository<Projection.Inventory> _projectionRepository;

    public ProjectInventoryItemWhenChangedConsumer(IProjectionRepository<Projection.Inventory> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    // TODO - It should be some think like (InventoryAdjustmentIncreased and InventoryAdjustmentDecreased)
    public async Task Consume(ConsumeContext<DomainEvent.InventoryAdjusted> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.ProductId,
            field: item => item.Quantity,
            value: context.Message.Quantity,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.InventoryReceived> context)
    {
        var inventory = new Projection.Inventory
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