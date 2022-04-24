using Application.EventStore;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.Commands;

public class UpdateCatalogItemConsumer : IConsumer<Command.UpdateCatalogItem>
{
    private readonly ICatalogEventStoreService _eventStoreService;

    public UpdateCatalogItemConsumer(ICatalogEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.UpdateCatalogItem> context)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
    }
}