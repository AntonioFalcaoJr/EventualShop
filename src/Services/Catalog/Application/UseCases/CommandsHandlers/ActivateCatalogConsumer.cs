using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages.Catalogs;

namespace Application.UseCases.CommandsHandlers
{
    public class ActivateCatalogConsumer : IConsumer<Commands.ActivateCatalog>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public ActivateCatalogConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<Commands.ActivateCatalog> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
            catalog.Activate(context.Message.CatalogId);
            await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
        }
    }
}