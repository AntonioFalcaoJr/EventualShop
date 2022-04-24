using Application.EventStore;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.Commands;

public class ChangeCatalogDescriptionConsumer : IConsumer<Command.ChangeCatalogDescription>
{
    private readonly ICatalogEventStoreService _eventStoreService;

    public ChangeCatalogDescriptionConsumer(ICatalogEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.ChangeCatalogDescription> context)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
    }
}