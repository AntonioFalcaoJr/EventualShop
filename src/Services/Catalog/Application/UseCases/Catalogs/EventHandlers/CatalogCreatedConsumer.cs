using System.Threading.Tasks;
using Application.EventSourcing.Catalogs.EventStore;
using Application.EventSourcing.Catalogs.Projections;
using Domain.Entities.Catalogs;
using MassTransit;

namespace Application.UseCases.Catalogs.EventHandlers
{
    public class CatalogCreatedConsumer : IConsumer<Events.CatalogCreated>
    {
        private readonly ICatalogEventStoreService _eventStoreService;
        private readonly ICatalogProjectionsService _projectionsService;

        public CatalogCreatedConsumer(ICatalogEventStoreService eventStoreService, ICatalogProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<Events.CatalogCreated> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.Id, context.CancellationToken);

            var catalogDetails = new CatalogProjection
            {
                Id = catalog.Id,
                Title = catalog.Title
            };

            await _projectionsService.ProjectNewCatalogDetailsAsync(catalogDetails, context.CancellationToken);
        }
    }
}