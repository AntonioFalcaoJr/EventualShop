using Application.EventStore;
using Contracts.Services.Catalog;
using MassTransit;

namespace Application.UseCases.Commands;

public class RemoveCatalogItemConsumer : IConsumer<Command.RemoveCatalogItem>
{
    private readonly ICatalogEventStoreService _eventStore;

    public RemoveCatalogItemConsumer(ICatalogEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.RemoveCatalogItem> context)
    {
        var catalog = await _eventStore.LoadAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStore.AppendAsync(catalog, context.CancellationToken);
    }
}