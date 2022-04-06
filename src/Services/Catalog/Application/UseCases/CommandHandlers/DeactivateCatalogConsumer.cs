using Application.EventSourcing.EventStore;
using MassTransit;
using ECommerce.Contracts.Catalogs;

namespace Application.UseCases.CommandHandlers;

public class DeactivateCatalogConsumer : IConsumer<Commands.DeactivateCatalog>
{
    private readonly ICatalogEventStoreService _eventStoreService;

    public DeactivateCatalogConsumer(ICatalogEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.DeactivateCatalog> context)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
    }
}