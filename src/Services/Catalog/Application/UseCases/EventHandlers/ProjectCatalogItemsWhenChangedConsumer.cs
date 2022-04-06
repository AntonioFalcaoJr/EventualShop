using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using Domain.Entities.CatalogItems;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.EventHandlers;

public class ProjectCatalogItemsWhenChangedConsumer :
    IConsumer<DomainEvents.CatalogDeleted>,
    IConsumer<DomainEvents.CatalogItemAdded>,
    IConsumer<DomainEvents.CatalogItemRemoved>,
    IConsumer<DomainEvents.CatalogItemUpdated>
{
    private readonly ICatalogEventStoreService _eventStoreService;
    private readonly ICatalogProjectionsService _projectionsService;

    public ProjectCatalogItemsWhenChangedConsumer(
        ICatalogEventStoreService eventStoreService,
        ICatalogProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<DomainEvents.CatalogItemAdded> context)
        => ProjectAsync(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogItemRemoved> context)
        => ProjectAsync(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogItemUpdated> context)
        => ProjectAsync(context.Message.CatalogId, context.CancellationToken);

    private async Task ProjectAsync(Guid catalogId, CancellationToken cancellationToken)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(catalogId, cancellationToken);

        var catalogItems = catalog.Items.Select<CatalogItem, Projections.CatalogItem>(item
            => new(item.Id, catalog.Id, item.Name, item.Description, item.Price, item.PictureUri, item.IsDeleted));

        await _projectionsService.ProjectManyAsync(catalogItems, cancellationToken);
    }

    public Task Consume(ConsumeContext<DomainEvents.CatalogDeleted> context)
        => _projectionsService.RemoveAsync<Projections.CatalogItem>(item
            => item.CatalogId == context.Message.CatalogId, context.CancellationToken);
}