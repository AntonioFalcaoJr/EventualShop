using Application.EventSourcing.EventStore;
using MassTransit;
using UpdateCatalogItemCommand = ECommerce.Contracts.Catalog.Commands.UpdateCatalogItem;

namespace Application.UseCases.Commands;

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
        catalog.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
    }
}