using Application.EventStore;
using Contracts.Services.Catalog;
using MassTransit;

namespace Application.UseCases.Commands;

public class ChangeCatalogTitleConsumer : IConsumer<Command.ChangeCatalogTitle>
{
    private readonly ICatalogEventStoreService _eventStore;

    public ChangeCatalogTitleConsumer(ICatalogEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.ChangeCatalogTitle> context)
    {
        var catalog = await _eventStore.LoadAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStore.AppendAsync(catalog, context.CancellationToken);
    }
}