using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using CatalogActivatedEvent = Messages.Catalogs.Events.CatalogActivated;
using CatalogDeactivatedEvent = Messages.Catalogs.Events.CatalogDeactivated;
using CatalogUpdatedEvent = Messages.Catalogs.Events.CatalogUpdated;
using CatalogDeletedEvent = Messages.Catalogs.Events.CatalogDeleted;
using CatalogItemAddedEvent = Messages.Catalogs.Events.CatalogItemAdded;
using CatalogItemRemovedEvent = Messages.Catalogs.Events.CatalogItemRemoved;
using CatalogItemUpdatedEvent = Messages.Catalogs.Events.CatalogItemUpdated;

namespace Application.UseCases.Events
{
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
}