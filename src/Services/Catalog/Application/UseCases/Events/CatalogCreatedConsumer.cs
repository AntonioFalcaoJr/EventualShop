using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using CatalogCreatedEvent = Messages.Catalogs.Events.CatalogCreated;

namespace Application.UseCases.Events;

public class CatalogCreatedConsumer : IConsumer<CatalogCreatedEvent>
{
    private readonly ICatalogEventStoreService _eventStoreService;
    private readonly ICatalogProjectionsService _projectionsService;

    public CatalogCreatedConsumer(ICatalogEventStoreService eventStoreService, ICatalogProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<CatalogCreatedEvent> context)
    {
        var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);

        var catalogDetails = new CatalogProjection
        {
            Id = catalog.Id,
            Title = catalog.Title
        };

        await _projectionsService.ProjectNewCatalogDetailsAsync(catalogDetails, context.CancellationToken);
    }
}