using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages.Catalogs;

namespace Application.UseCases.CommandsHandlers
{
    public class UpdateCatalogConsumer : IConsumer<Commands.UpdateCatalog>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public UpdateCatalogConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<Commands.UpdateCatalog> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
            catalog.Update(context.Message.CatalogId, context.Message.Title);
            await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
        }
    }
}