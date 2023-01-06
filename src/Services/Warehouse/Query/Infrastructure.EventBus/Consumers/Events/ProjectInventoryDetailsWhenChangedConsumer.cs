using Application.UseCases.Events;
using Contracts.Services.Warehouse;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectInventoryDetailsWhenChangedConsumer : IConsumer<DomainEvent.InventoryCreated>
{
    private readonly IProjectInventoryDetailsWhenChangedInteractor _interactor;

    public ProjectInventoryDetailsWhenChangedConsumer(ProjectInventoryDetailsWhenChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.InventoryCreated> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}