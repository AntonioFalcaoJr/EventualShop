using Application.UseCases.Events;
using Contracts.Services.Warehouse;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectInventoryItemWhenChangedConsumer :
    IConsumer<DomainEvent.InventoryAdjustmentDecreased>,
    IConsumer<DomainEvent.InventoryAdjustmentIncreased>,
    IConsumer<DomainEvent.InventoryItemIncreased>,
    IConsumer<DomainEvent.InventoryItemReceived>
{
    private readonly IProjectInventoryItemListItemWhenInventoryItemChangedInteractor _interactor;

    public ProjectInventoryItemWhenChangedConsumer(IProjectInventoryItemListItemWhenInventoryItemChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.InventoryAdjustmentDecreased> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.InventoryAdjustmentIncreased> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.InventoryItemIncreased> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
    
    public Task Consume(ConsumeContext<DomainEvent.InventoryItemReceived> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}