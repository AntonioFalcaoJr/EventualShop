using Application.EventStore;
using Contracts.Services.Catalog;
using MassTransit;

namespace Application.UseCases.Commands;

public class ActivateCatalogConsumer : IConsumer<Command.ActivateCatalog>
{
    private readonly ICatalogEventStoreService _eventStore;

    public ActivateCatalogConsumer(ICatalogEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.ActivateCatalog> context)
    {
        var catalog = await _eventStore.LoadAsync(context.Message.Id, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStore.AppendAsync(catalog, context.CancellationToken);
    }
}