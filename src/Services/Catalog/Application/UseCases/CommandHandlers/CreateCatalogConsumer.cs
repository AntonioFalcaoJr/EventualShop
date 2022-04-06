using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using ECommerce.Contracts.Catalogs;

namespace Application.UseCases.CommandHandlers;

public class CreateCatalogConsumer : IConsumer<Commands.CreateCatalog>
{
    private readonly ICatalogEventStoreService _eventStoreService;

    public CreateCatalogConsumer(ICatalogEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.CreateCatalog> context)
    {
        var catalog = new Catalog();
        catalog.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
    }
}