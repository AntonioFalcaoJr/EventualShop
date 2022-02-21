using System.Threading.Tasks;
using Application.EventSourcing.Projections;
using ECommerce.Contracts.Catalog;
using MassTransit;
using GetCatalogItemsDetailsQuery = ECommerce.Contracts.Catalog.Queries.GetCatalogItemsDetails;

namespace Application.UseCases.Queries;

public class GetCatalogItemsDetailsConsumer : IConsumer<GetCatalogItemsDetailsQuery>
{
    private readonly ICatalogProjectionsService _projectionsService;

    public GetCatalogItemsDetailsConsumer(ICatalogProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetCatalogItemsDetailsQuery> context)
    {
        var catalogItems = await _projectionsService.GetCatalogItemsAsync(context.Message.CatalogId, context.Message.Limit, context.Message.Offset, context.CancellationToken);
        await context.RespondAsync<Responses.CatalogItemsDetailsPagedResult>(catalogItems);
    }
}