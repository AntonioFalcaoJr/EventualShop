using Application.Abstractions;
using Contracts.Services.Catalog;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemCardConsumer : IConsumer<DomainEvent.CatalogItemAdded>
{
    private readonly IInteractor<DomainEvent.CatalogItemAdded> _interactor;

    public ProjectCatalogItemCardConsumer(IInteractor<DomainEvent.CatalogItemAdded> interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.CatalogItemAdded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}