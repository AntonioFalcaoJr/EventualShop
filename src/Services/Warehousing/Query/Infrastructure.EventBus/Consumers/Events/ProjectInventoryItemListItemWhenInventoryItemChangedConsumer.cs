using Application.UseCases.Events;
using Contracts.Boundaries.Warehouse;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectInventoryItemListItemWhenInventoryItemChangedConsumer(IProjectInventoryItemListItemWhenInventoryItemChangedInteractor interactor)
    :
        IConsumer<DomainEvent.InventoryAdjustmentDecreased>,
        IConsumer<DomainEvent.InventoryAdjustmentIncreased>,
        IConsumer<DomainEvent.InventoryItemIncreased>,
        IConsumer<DomainEvent.InventoryItemReceived>
{
    public Task Consume(ConsumeContext<DomainEvent.InventoryAdjustmentDecreased> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.InventoryAdjustmentIncreased> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.InventoryItemIncreased> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
    
    public Task Consume(ConsumeContext<DomainEvent.InventoryItemReceived> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
}