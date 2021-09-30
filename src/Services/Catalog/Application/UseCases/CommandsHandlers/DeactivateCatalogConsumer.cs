using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages.Catalogs;

namespace Application.UseCases.CommandsHandlers
{
    public class DeactivateCatalogConsumer : IConsumer<Commands.DeactivateCatalog>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public DeactivateCatalogConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<Commands.DeactivateCatalog> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
            catalog.Deactivate(context.Message.CatalogId);
            await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
        }
    }
}