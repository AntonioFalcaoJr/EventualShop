using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages.Catalogs.Commands;

namespace Application.UseCases.Commands
{
    public class DeleteCatalogConsumer : IConsumer<DeleteCatalog>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public DeleteCatalogConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<DeleteCatalog> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.Id, context.CancellationToken);
            catalog.Delete(context.Message.Id);
            await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
        }
    }
}