using Application.EventStore;
using Contracts.Services.Catalog;
using MassTransit;

namespace Application.UseCases.Commands;

public class DeleteCatalogConsumer : IConsumer<Command.DeleteCatalog>
{
    private readonly ICatalogEventStoreService _eventStore;

    public DeleteCatalogConsumer(ICatalogEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.DeleteCatalog> context)
    {
        var catalog = await _eventStore.LoadAggregateAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStore.AppendEventsAsync(catalog, context.CancellationToken);
    }
}