using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using DeactivateCatalogCommand = ECommerce.Contracts.Catalog.Commands.DeactivateCatalog;

namespace Application.UseCases.Commands;

public class DeactivateCatalogConsumer : IConsumer<DeactivateCatalogCommand>
{
    private readonly ICatalogEventStoreService _eventStoreService;

    public DeactivateCatalogConsumer(ICatalogEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DeactivateCatalogCommand> context)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
        catalog.Deactivate(context.Message.CatalogId);
        await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
    }
}