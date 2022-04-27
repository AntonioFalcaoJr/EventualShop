using Application.EventStore;
using Contracts.Services.Catalog;
using MassTransit;

namespace Application.UseCases.Commands;

public class DeactivateCatalogConsumer : IConsumer<Command.DeactivateCatalog>
{
    private readonly ICatalogEventStoreService _eventStore;

    public DeactivateCatalogConsumer(ICatalogEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.DeactivateCatalog> context)
    {
        var catalog = await _eventStore.LoadAggregateAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStore.AppendEventsAsync(catalog, context.CancellationToken);
    }
}