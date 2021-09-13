using System.Threading.Tasks;
using Application.EventSourcing.Catalogs.EventStore;
using MassTransit;
using Messages.Catalogs.Commands;

namespace Application.UseCases.Catalogs.Commands
{
    public class DeactivateCatalogConsumer : IConsumer<DeactivateCatalog>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public DeactivateCatalogConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<DeactivateCatalog> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.Id, context.CancellationToken);
            catalog.Deactivate(context.Message.Id);
            await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
        }
    }
}