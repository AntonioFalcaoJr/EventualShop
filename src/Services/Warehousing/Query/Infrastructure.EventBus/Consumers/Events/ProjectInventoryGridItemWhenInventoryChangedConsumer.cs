using Application.UseCases.Events;
using Contracts.Boundaries.Warehouse;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectInventoryGridItemWhenInventoryChangedConsumer(IProjectInventoryGridItemWhenInventoryChangedInteractor interactor)
    : IConsumer<DomainEvent.InventoryCreated>
{
    public Task Consume(ConsumeContext<DomainEvent.InventoryCreated> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
}