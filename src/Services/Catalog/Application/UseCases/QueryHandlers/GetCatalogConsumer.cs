using Application.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetCatalogConsumer : IConsumer<Queries.GetCatalog>
{
    private readonly ICatalogProjectionsService _projectionsService;

    public GetCatalogConsumer(ICatalogProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<Queries.GetCatalog> context)
    {
        var catalog = await _projectionsService.GetCatalogAsync(context.Message.CatalogId, context.CancellationToken);

        await (catalog is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(catalog));
    }
}