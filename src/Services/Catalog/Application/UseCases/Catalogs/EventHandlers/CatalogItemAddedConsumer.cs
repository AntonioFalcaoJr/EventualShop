using System.Linq;
using System.Threading.Tasks;
using Application.EventSourcing.Catalogs.EventStore;
using Application.EventSourcing.Catalogs.Projections;
using Domain.Entities.Catalogs;
using MassTransit;

namespace Application.UseCases.Catalogs.EventHandlers
{
    public class CatalogItemAddedConsumer : IConsumer<Events.CatalogItemAdded>
    {
        private readonly ICatalogEventStoreService _eventStoreService;
        private readonly ICatalogProjectionsService _projectionsService;

        public CatalogItemAddedConsumer(ICatalogEventStoreService eventStoreService, ICatalogProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<Events.CatalogItemAdded> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);

            var catalogDetails = new CatalogProjection
            {
                Id = catalog.Id,
                Title = catalog.Title,
                IsActive = catalog.IsActive,
                Items = catalog.Items.Select(item 
                    => new CatalogItemProjection
                {
                    Description = item.Description,
                    Name = item.Name,
                    Price = item.Price,
                    PictureUri = item.PictureUri
                })
            };

            await _projectionsService.ProjectNewCatalogDetailsAsync(catalogDetails, context.CancellationToken);
        }
    }
}