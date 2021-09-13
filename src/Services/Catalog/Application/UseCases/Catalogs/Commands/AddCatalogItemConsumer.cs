using System.Threading.Tasks;
using Application.EventSourcing.Catalogs.EventStore;
using Domain.Entities.CatalogItems;
using MassTransit;
using Messages.Catalogs.Commands;

namespace Application.UseCases.Catalogs.Commands
{
    public class AddCatalogItemConsumer : IConsumer<AddCatalogItem>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public AddCatalogItemConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<AddCatalogItem> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);

            var catalogItem = new CatalogItem(
                context.Message.Name,
                context.Message.Description,
                context.Message.Price,
                context.Message.PictureUri);

            catalog.AddItem(catalog.Id, catalogItem);
            
            await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
        }
    }
}