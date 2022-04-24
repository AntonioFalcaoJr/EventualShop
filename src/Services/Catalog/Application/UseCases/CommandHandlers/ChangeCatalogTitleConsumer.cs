using Application.EventStore;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class ChangeCatalogTitleConsumer : IConsumer<Command.ChangeCatalogTitle>
{
    private readonly ICatalogEventStoreService _eventStoreService;

    public ChangeCatalogTitleConsumer(ICatalogEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.ChangeCatalogTitle> context)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
    }
}