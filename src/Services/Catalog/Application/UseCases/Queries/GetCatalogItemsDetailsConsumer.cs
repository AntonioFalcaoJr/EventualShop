using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Catalogs.Queries;
using Messages.Catalogs.Queries.Responses;

namespace Application.UseCases.Queries
{
    public class GetCatalogItemsDetailsConsumer : IConsumer<GetCatalogItemsDetailsWithPagination>
    {
        private readonly ICatalogProjectionsService _projectionsService;

        public GetCatalogItemsDetailsConsumer(ICatalogProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<GetCatalogItemsDetailsWithPagination> context)
        {
            var accountDetails = await _projectionsService.GetCatalogItemsWithPaginationAsync(
                paging: new Paging { Limit = context.Message.Limit, Offset = context.Message.Offset },
                predicate: catalog => catalog.AggregateId == context.Message.Id
                                      && catalog.IsActive
                                      && !catalog.IsDeleted,
                selector: catalog => catalog.Items,
                cancellationToken: context.CancellationToken);

            await context.RespondAsync<CatalogItemsDetailsPagedResult>(accountDetails);
        }
    }
}