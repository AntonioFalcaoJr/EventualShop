using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages.Catalogs.Commands;

namespace Application.UseCases.Commands
{
    public class ActivateCatalogConsumer : IConsumer<ActivateCatalog>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public ActivateCatalogConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<ActivateCatalog> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.Id, context.CancellationToken);
            catalog.Activate(context.Message.Id);
            await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
        }
    }
}