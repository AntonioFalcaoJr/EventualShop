using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using Domain.Entities.Catalogs;
using MassTransit;

namespace Application.UseCases.EventHandlers
{
    public class CatalogChangedConsumer :
        IConsumer<Events.CatalogActivated>,
        IConsumer<Events.CatalogDeactivated>,
        IConsumer<Events.CatalogUpdated>,
        IConsumer<Events.CatalogDeleted>,
        IConsumer<Events.CatalogItemAdded>,
        IConsumer<Events.CatalogItemRemoved>,
        IConsumer<Events.CatalogItemEdited>
    {
        private readonly ICatalogEventStoreService _eventStoreService;
        private readonly ICatalogProjectionsService _projectionsService;

        public CatalogChangedConsumer(ICatalogEventStoreService eventStoreService, ICatalogProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public Task Consume(ConsumeContext<Events.CatalogActivated> context)
            => Project(context.Message.Id, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.CatalogDeactivated> context)
            => Project(context.Message.Id, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.CatalogUpdated> context)
            => Project(context.Message.Id, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.CatalogDeleted> context)
            => Project(context.Message.Id, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.CatalogItemAdded> context)
            => Project(context.Message.Id, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.CatalogItemRemoved> context)
            => Project(context.Message.Id, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.CatalogItemEdited> context)
            => Project(context.Message.Id, context.CancellationToken);

        private async Task Project(Guid catalogId, CancellationToken cancellationToken)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(catalogId, cancellationToken);

            var catalogDetails = new CatalogProjection
            {
                Id = catalog.Id,
                Title = catalog.Title,
                IsActive = catalog.IsActive,
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