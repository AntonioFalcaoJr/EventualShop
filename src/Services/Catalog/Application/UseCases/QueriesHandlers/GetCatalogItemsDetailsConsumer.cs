using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Catalogs;

namespace Application.UseCases.QueriesHandlers
{
    public class GetCatalogItemsDetailsConsumer : IConsumer<Queries.GetCatalogItemsDetailsWithPagination>
    {
        private readonly ICatalogProjectionsService _projectionsService;

        public GetCatalogItemsDetailsConsumer(ICatalogProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<Queries.GetCatalogItemsDetailsWithPagination> context)
        {
            var accountDetails = await _projectionsService.GetCatalogItemsWithPaginationAsync(
                paging: new Paging { Limit = context.Message.Limit, Offset = context.Message.Offset },
                predicate: catalog => catalog.AggregateId == context.Message.CatalogId
                                      && catalog.IsActive
                                      && catalog.IsDeleted == false,
                selector: catalog => catalog.Items,
                cancellationToken: context.CancellationToken);

            await context.RespondAsync<Responses.CatalogItemsDetailsPagedResult>(accountDetails);
        }
    }
}