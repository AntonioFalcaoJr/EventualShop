using Application.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetCatalogItemsConsumer : IConsumer<ECommerce.Contracts.Catalogs.Queries.GetCatalogItems>
{
    private readonly ICatalogProjectionsService _projectionsService;

    public GetCatalogItemsConsumer(ICatalogProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<ECommerce.Contracts.Catalogs.Queries.GetCatalogItems> context)
    {
        var catalogItems = await _projectionsService.GetCatalogItemsAsync(context.Message.CatalogId, context.Message.Limit, context.Message.Offset, context.CancellationToken);

        await (catalogItems is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync<Responses.CatalogItems>(catalogItems));
    }
}