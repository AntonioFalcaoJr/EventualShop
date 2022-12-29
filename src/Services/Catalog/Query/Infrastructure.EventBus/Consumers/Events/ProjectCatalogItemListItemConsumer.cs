using Application.UseCases.Events;
using Contracts.Services.Catalog;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemListItemConsumer :
    IConsumer<DomainEvent.CatalogDeleted>,
    IConsumer<DomainEvent.CatalogItemAdded>,
    IConsumer<DomainEvent.CatalogItemRemoved>
{
    private readonly IProjectCatalogItemListItemInteractor _interactor;

    public ProjectCatalogItemListItemConsumer(IProjectCatalogItemListItemInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.CatalogDeleted> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogItemAdded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogItemRemoved> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}