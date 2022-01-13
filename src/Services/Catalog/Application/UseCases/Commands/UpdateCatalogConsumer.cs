using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using UpdateCatalogCommand = ECommerce.Contracts.Catalog.Commands.UpdateCatalog;

namespace Application.UseCases.Commands;

public class UpdateCatalogConsumer : IConsumer<UpdateCatalogCommand>
{
    private readonly ICatalogEventStoreService _eventStoreService;

    public UpdateCatalogConsumer(ICatalogEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<UpdateCatalogCommand> context)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.Message, context.CancellationToken);
    }
}