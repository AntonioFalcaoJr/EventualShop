using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages.Catalogs;

namespace Application.UseCases.CommandsHandlers
{
    public class UpdateCatalogItemConsumer : IConsumer<Commands.UpdateCatalogItem>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public UpdateCatalogItemConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<Commands.UpdateCatalogItem> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);

            catalog.UpdateItem(
                catalog.Id,
                context.Message.CatalogItemId,
                context.Message.Name,
                context.Message.Description,
                context.Message.Price,
                context.Message.PictureUri);

            await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
        }
    }
}