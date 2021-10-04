using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using UpdateCatalogItemCommand = Messages.Catalogs.Commands.UpdateCatalogItem;

namespace Application.UseCases.Commands
{
    public class UpdateCatalogItemConsumer : IConsumer<UpdateCatalogItemCommand>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public UpdateCatalogItemConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<UpdateCatalogItemCommand> context)
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