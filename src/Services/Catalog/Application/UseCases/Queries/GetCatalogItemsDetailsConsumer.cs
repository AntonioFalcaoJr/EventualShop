using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Projections;
using ECommerce.Contracts.Catalog;
using MassTransit;
using GetCatalogItemsDetailsWithPaginationQuery = ECommerce.Contracts.Catalog.Queries.GetCatalogItemsDetailsWithPagination;

namespace Application.UseCases.Queries;

public class GetCatalogItemsDetailsConsumer : IConsumer<GetCatalogItemsDetailsWithPaginationQuery>
{
    private readonly ICatalogProjectionsService _projectionsService;

    public GetCatalogItemsDetailsConsumer(ICatalogProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetCatalogItemsDetailsWithPaginationQuery> context)
    {
        var catalogItems = await _projectionsService.GetCatalogItemsWithPaginationAsync(
            paging: new() { Limit = context.Message.Limit, Offset = context.Message.Offset },
            predicate: catalog => catalog.Id == context.Message.CatalogId
                                  && catalog.IsActive
                                  && catalog.IsDeleted == false,
            selector: catalog => catalog.Items,
            cancellationToken: context.CancellationToken);

        await context.RespondAsync<Responses.CatalogItemsDetailsPagedResult>(catalogItems);
    }
}