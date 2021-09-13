using System.Threading.Tasks;
using Application.EventSourcing.Catalogs.EventStore;
using Domain.Entities.CatalogItems;
using MassTransit;
using Messages.Catalogs.Commands;

namespace Application.UseCases.Catalogs.Commands
{
    public class RemoveCatalogItemConsumer : IConsumer<RemoveCatalogItem>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public RemoveCatalogItemConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<RemoveCatalogItem> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
            catalog.RemoveItem(catalog.Id, context.Message.CatalogItemId);
            await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
        }
    }
}