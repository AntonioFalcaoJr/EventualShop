using Application.UseCases.Events;
using Contracts.Services.Catalog;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemDetailsConsumer : IConsumer<DomainEvent.CatalogItemAdded>
{
    private readonly IProjectCatalogItemDetailsInteractor _interactor;

    public ProjectCatalogItemDetailsConsumer(IProjectCatalogItemDetailsInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.CatalogItemAdded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}