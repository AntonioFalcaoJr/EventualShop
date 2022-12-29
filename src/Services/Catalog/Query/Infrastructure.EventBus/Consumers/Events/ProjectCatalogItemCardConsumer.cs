using Application.UseCases.Events;
using Contracts.Services.Catalog;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemCardConsumer : IConsumer<DomainEvent.CatalogItemAdded>
{
    private readonly IProjectCatalogItemCardInteractor _interactor;

    public ProjectCatalogItemCardConsumer(IProjectCatalogItemCardInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.CatalogItemAdded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}