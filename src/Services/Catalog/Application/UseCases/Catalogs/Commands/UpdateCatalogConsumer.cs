using System.Threading.Tasks;
using Application.EventSourcing.Catalogs.EventStore;
using MassTransit;
using Messages.Catalogs.Commands;

namespace Application.UseCases.Catalogs.Commands
{
    public class UpdateCatalogConsumer : IConsumer<UpdateCatalog>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public UpdateCatalogConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<UpdateCatalog> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.Id, context.CancellationToken);
            catalog.Update(context.Message.Id, context.Message.Title);
            await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
        }
    }
}