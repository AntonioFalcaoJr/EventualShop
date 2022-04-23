using Application.EventStore;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class ChangeCatalogDescriptionConsumer : IConsumer<Commands.ChangeCatalogDescription>
{
    private readonly ICatalogEventStoreService _eventStoreService;

    public ChangeCatalogDescriptionConsumer(ICatalogEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.ChangeCatalogDescription> context)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
    }
}