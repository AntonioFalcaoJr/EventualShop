using Application.Abstractions.Projections;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectInventoryItemWhenChangedConsumer :
    IConsumer<DomainEvents.InventoryAdjusted>,
    IConsumer<DomainEvents.InventoryReceived>
{
    private readonly IProjectionsRepository<ECommerce.Contracts.Warehouses.Projections.InventoryProjection> _projectionsRepository;

    public ProjectInventoryItemWhenChangedConsumer(IProjectionsRepository<ECommerce.Contracts.Warehouses.Projections.InventoryProjection> projectionsRepository)
    {
        _projectionsRepository = projectionsRepository;
    }

    // TODO - It should be some think like (InventoryAdjustmentIncreased and InventoryAdjustmentDecreased)
    public async Task Consume(ConsumeContext<DomainEvents.InventoryAdjusted> context)
        => await _projectionsRepository.UpdateFieldAsync(
            id: context.Message.ProductId,
            field: item => item.Quantity,
            value: context.Message.Quantity,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.InventoryReceived> context)
    {
        var inventory = new ECommerce.Contracts.Warehouses.Projections.InventoryProjection
        {
            Id = context.Message.ProductId,
            Description = context.Message.Description,
            Name = context.Message.Name,
            Quantity = context.Message.Quantity,
            Sku = context.Message.Sku,
            IsDeleted = false
        };

        await _projectionsRepository.InsertAsync(inventory, context.CancellationToken);
    }
}