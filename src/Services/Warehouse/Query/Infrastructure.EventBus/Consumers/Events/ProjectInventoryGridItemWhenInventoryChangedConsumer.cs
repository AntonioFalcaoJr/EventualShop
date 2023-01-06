using Application.UseCases.Events;
using Contracts.Services.Warehouse;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectInventoryGridItemWhenInventoryChangedConsumer : IConsumer<DomainEvent.InventoryCreated>
{
    private readonly IProjectInventoryGridItemWhenInventoryChangedInteractor _interactor;

    public ProjectInventoryGridItemWhenInventoryChangedConsumer(IProjectInventoryGridItemWhenInventoryChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.InventoryCreated> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}