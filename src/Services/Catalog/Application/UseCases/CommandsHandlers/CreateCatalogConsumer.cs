using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Entities.Catalogs;
using MassTransit;
using Messages.Catalogs;

namespace Application.UseCases.CommandsHandlers
{
    public class CreateCatalogConsumer : IConsumer<Commands.CreateCatalog>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public CreateCatalogConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<Commands.CreateCatalog> context)
        {
            var catalog = new Catalog();
            catalog.Create(context.Message.Title);
            await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
        }
    }
}