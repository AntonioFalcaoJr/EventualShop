using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using ECommerce.Contracts.Catalogs;
using MassTransit;
using CatalogCreatedEvent = ECommerce.Contracts.Catalogs.DomainEvents.CatalogCreated;
using CatalogActivatedEvent = ECommerce.Contracts.Catalogs.DomainEvents.CatalogActivated;
using CatalogDeactivatedEvent = ECommerce.Contracts.Catalogs.DomainEvents.CatalogDeactivated;
using CatalogUpdatedEvent = ECommerce.Contracts.Catalogs.DomainEvents.CatalogUpdated;
using CatalogDeletedEvent = ECommerce.Contracts.Catalogs.DomainEvents.CatalogDeleted;
using CatalogItemAddedEvent = ECommerce.Contracts.Catalogs.DomainEvents.CatalogItemAdded;
using CatalogItemRemovedEvent = ECommerce.Contracts.Catalogs.DomainEvents.CatalogItemRemoved;
using CatalogItemUpdatedEvent = ECommerce.Contracts.Catalogs.DomainEvents.CatalogItemUpdated;

namespace Application.UseCases.Events;

public class ProjectCatalogWhenChangedConsumer :
    IConsumer<CatalogCreatedEvent>,
    IConsumer<CatalogActivatedEvent>,
    IConsumer<CatalogDeactivatedEvent>,
    IConsumer<CatalogUpdatedEvent>,
    IConsumer<CatalogDeletedEvent>
{
    private readonly ICatalogEventStoreService _eventStoreService;
    private readonly ICatalogProjectionsService _projectionsService;

    public ProjectCatalogWhenChangedConsumer(
        ICatalogEventStoreService eventStoreService,
        ICatalogProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<CatalogCreatedEvent> context)
        => ProjectAsync(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<CatalogActivatedEvent> context)
        => ProjectAsync(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<CatalogDeactivatedEvent> context)
        => ProjectAsync(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<CatalogDeletedEvent> context)
        => ProjectAsync(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<CatalogUpdatedEvent> context)
        => ProjectAsync(context.Message.CatalogId, context.CancellationToken);

    private async Task ProjectAsync(Guid catalogId, CancellationToken cancellationToken)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(catalogId, cancellationToken);
        var projection = new Projections.Catalog(catalog.Id, catalog.Title, catalog.Description, catalog.IsActive, catalog.IsDeleted);
        await _projectionsService.ProjectAsync(projection, cancellationToken);
    }
}