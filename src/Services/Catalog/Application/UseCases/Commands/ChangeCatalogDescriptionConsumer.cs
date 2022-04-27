using Application.EventStore;
using Contracts.Services.Catalogs;
using MassTransit;

namespace Application.UseCases.Commands;

public class ChangeCatalogDescriptionConsumer : IConsumer<Command.ChangeCatalogDescription>
{
    private readonly ICatalogEventStoreService _eventStore;

    public ChangeCatalogDescriptionConsumer(ICatalogEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.ChangeCatalogDescription> context)
    {
        var catalog = await _eventStore.LoadAggregateAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStore.AppendEventsAsync(catalog, context.CancellationToken);
    }
}