using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.EventsHandlers;

public class ProjectCatalogWhenChangedConsumer :
    IConsumer<DomainEvents.CatalogCreated>,
    IConsumer<DomainEvents.CatalogActivated>,
    IConsumer<DomainEvents.CatalogDeactivated>,
    IConsumer<DomainEvents.CatalogUpdated>,
    IConsumer<DomainEvents.CatalogDeleted>
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

    public Task Consume(ConsumeContext<DomainEvents.CatalogCreated> context)
        => ProjectAsync(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogActivated> context)
        => ProjectAsync(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogDeactivated> context)
        => ProjectAsync(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogDeleted> context)
        => ProjectAsync(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogUpdated> context)
        => ProjectAsync(context.Message.CatalogId, context.CancellationToken);

    private async Task ProjectAsync(Guid catalogId, CancellationToken cancellationToken)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(catalogId, cancellationToken);
        var projection = new Projections.Catalog(catalog.Id, catalog.Title, catalog.Description, catalog.IsActive, catalog.IsDeleted);
        await _projectionsService.ProjectAsync(projection, cancellationToken);
    }
}