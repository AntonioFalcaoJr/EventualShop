using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using CatalogActivatedEvent = Messages.Services.Catalogs.Events.CatalogActivated;
using CatalogDeactivatedEvent = Messages.Services.Catalogs.Events.CatalogDeactivated;
using CatalogUpdatedEvent = Messages.Services.Catalogs.Events.CatalogUpdated;
using CatalogDeletedEvent = Messages.Services.Catalogs.Events.CatalogDeleted;
using CatalogItemAddedEvent = Messages.Services.Catalogs.Events.CatalogItemAdded;
using CatalogItemRemovedEvent = Messages.Services.Catalogs.Events.CatalogItemRemoved;
using CatalogItemUpdatedEvent = Messages.Services.Catalogs.Events.CatalogItemUpdated;

namespace Application.UseCases.Events;

public class CatalogChangedConsumer :
    IConsumer<CatalogActivatedEvent>,
    IConsumer<CatalogDeactivatedEvent>,
    IConsumer<CatalogUpdatedEvent>,
    IConsumer<CatalogDeletedEvent>,
    IConsumer<CatalogItemAddedEvent>,
    IConsumer<CatalogItemRemovedEvent>,
    IConsumer<CatalogItemUpdatedEvent>
{
    private readonly ICatalogEventStoreService _eventStoreService;
    private readonly ICatalogProjectionsService _projectionsService;

    public CatalogChangedConsumer(ICatalogEventStoreService eventStoreService, ICatalogProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<CatalogActivatedEvent> context)
        => Project(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<CatalogDeactivatedEvent> context)
        => Project(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<CatalogDeletedEvent> context)
        => Project(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<CatalogItemAddedEvent> context)
        => Project(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<CatalogItemRemovedEvent> context)
        => Project(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<CatalogItemUpdatedEvent> context)
        => Project(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<CatalogUpdatedEvent> context)
        => Project(context.Message.CatalogId, context.CancellationToken);

    private async Task Project(Guid catalogId, CancellationToken cancellationToken)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(catalogId, cancellationToken);

        var catalogDetails = new CatalogProjection
        {
            Id = catalog.Id,
            Title = catalog.Title,
            IsActive = catalog.IsActive,
            IsDeleted = catalog.IsDeleted,
            Items = catalog.Items
                .Select(item
                    => new CatalogItemProjection
                    {
                        Id = item.Id,
                        Description = item.Description,
                        Name = item.Name,
                        Price = item.Price,
                        PictureUri = item.PictureUri
                    })
        };

        await _projectionsService.ProjectCatalogDetailsAsync(catalogDetails, cancellationToken);
    }
}