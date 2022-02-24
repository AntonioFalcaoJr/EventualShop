using System.Threading.Tasks;
using Application.EventSourcing.Projections;
using ECommerce.Contracts.Catalog;
using MassTransit;
using GetCatalogItemsQuery = ECommerce.Contracts.Catalog.Queries.GetCatalogItems;

namespace Application.UseCases.Queries;

public class GetCatalogItemsConsumer : IConsumer<GetCatalogItemsQuery>
{
    private readonly ICatalogProjectionsService _projectionsService;

    public GetCatalogItemsConsumer(ICatalogProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetCatalogItemsQuery> context)
    {
        var catalogItems = await _projectionsService.GetCatalogItemsAsync(context.Message.CatalogId, context.Message.Limit, context.Message.Offset, context.CancellationToken);
        await context.RespondAsync<Responses.CatalogItemsDetailsPagedResult>(catalogItems);
    }
}