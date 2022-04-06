using Application.EventSourcing.EventStore;
using MassTransit;
using ECommerce.Contracts.Catalogs;

namespace Application.UseCases.CommandHandlers;

public class UpdateCatalogConsumer : IConsumer<Commands.UpdateCatalog>
{
    private readonly ICatalogEventStoreService _eventStoreService;

    public UpdateCatalogConsumer(ICatalogEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.UpdateCatalog> context)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
    }
}