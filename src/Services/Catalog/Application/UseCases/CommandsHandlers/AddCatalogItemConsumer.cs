using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages.Catalogs;

namespace Application.UseCases.CommandsHandlers
{
    public class AddCatalogItemConsumer : IConsumer<Commands.AddCatalogItem>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public AddCatalogItemConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<Commands.AddCatalogItem> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);

            catalog.AddItem(
                catalog.Id,
                context.Message.Name,
                context.Message.Description,
                context.Message.Price,
                context.Message.PictureUri);

            await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
        }
    }
}