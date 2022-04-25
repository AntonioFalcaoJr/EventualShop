using Application.Abstractions.Projections;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectInventoryItemWhenChangedConsumer :
    IConsumer<DomainEvent.InventoryAdjusted>,
    IConsumer<DomainEvent.InventoryReceived>
{
    private readonly IProjectionRepository<Projection.Inventory> _repository;

    public ProjectInventoryItemWhenChangedConsumer(IProjectionRepository<Projection.Inventory> repository)
    {
        _repository = repository;
    }

    // TODO - It should be some think like (InventoryAdjustmentIncreased and InventoryAdjustmentDecreased)
    public async Task Consume(ConsumeContext<DomainEvent.InventoryAdjusted> context)
        => await _repository.UpdateFieldAsync(
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

        await _repository.InsertAsync(inventory, context.CancellationToken);
    }
}