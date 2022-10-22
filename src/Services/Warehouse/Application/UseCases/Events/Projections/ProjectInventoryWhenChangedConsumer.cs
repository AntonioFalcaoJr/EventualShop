using Application.Abstractions.Projections;
using Contracts.Services.Warehouse;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectInventoryWhenChangedConsumer : IConsumer<DomainEvent.InventoryCreated>
{
    private readonly IProjectionRepository<Projection.Inventory> _repository;

    public ProjectInventoryWhenChangedConsumer(IProjectionRepository<Projection.Inventory> repository)
        => _repository = repository;

    public async Task Consume(ConsumeContext<DomainEvent.InventoryCreated> context)
    {
        Projection.Inventory inventory = new(
            context.Message.InventoryId,
            context.Message.OwnerId,
            false);

        await _repository.InsertAsync(inventory, context.CancellationToken);
    }
}