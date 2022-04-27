using Application.EventStore;
using Contracts.Services.Catalog;
using Domain.Aggregates;
using MassTransit;

namespace Application.UseCases.Commands;

public class CreateCatalogConsumer : IConsumer<Command.CreateCatalog>
{
    private readonly ICatalogEventStoreService _eventStore;

    public CreateCatalogConsumer(ICatalogEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.CreateCatalog> context)
    {
        var catalog = new Catalog();
        catalog.Handle(context.Message);
        await _eventStore.AppendEventsAsync(catalog, context.CancellationToken);
    }
}