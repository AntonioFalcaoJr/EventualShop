using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using ActivateCatalogCommand = ECommerce.Contracts.Catalog.Commands.ActivateCatalog;

namespace Application.UseCases.Commands;

public class ActivateCatalogConsumer : IConsumer<ActivateCatalogCommand>
{
    private readonly ICatalogEventStoreService _eventStoreService;

    public ActivateCatalogConsumer(ICatalogEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<ActivateCatalogCommand> context)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.Message, context.CancellationToken);
    }
}