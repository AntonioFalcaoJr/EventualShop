using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using RemoveCatalogItemCommand = ECommerce.Contracts.Catalog.Commands.RemoveCatalogItem;

namespace Application.UseCases.Commands;

public class RemoveCatalogItemConsumer : IConsumer<RemoveCatalogItemCommand>
{
    private readonly ICatalogEventStoreService _eventStoreService;

    public RemoveCatalogItemConsumer(ICatalogEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<RemoveCatalogItemCommand> context)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.RemoveItem(catalog.Id, context.Message.CatalogItemId);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
    }
}