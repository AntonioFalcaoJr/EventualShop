using Application.EventSourcing.EventStore;
using MassTransit;
using ECommerce.Contracts.Catalogs;

namespace Application.UseCases.CommandHandlers;

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
        catalog.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
    }
}