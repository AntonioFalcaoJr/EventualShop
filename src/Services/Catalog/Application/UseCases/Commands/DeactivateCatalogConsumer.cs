using Application.EventStore;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.Commands;

public class DeactivateCatalogConsumer : IConsumer<Command.DeactivateCatalog>
{
    private readonly ICatalogEventStoreService _eventStoreService;

    public DeactivateCatalogConsumer(ICatalogEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.DeactivateCatalog> context)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
    }
}