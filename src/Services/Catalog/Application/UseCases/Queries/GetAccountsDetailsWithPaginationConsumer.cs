using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Accounts.Queries;
using Messages.Accounts.Queries.Responses;

namespace Application.UseCases.Queries
{
    public class GetAccountsDetailsWithPaginationConsumer : IConsumer<GetAccountsDetailsWithPagination>
    {
        private readonly ICatalogProjectionsService _projectionsService;

        public GetAccountsDetailsWithPaginationConsumer(ICatalogProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<GetAccountsDetailsWithPagination> context)
        {
            var paginatedResult = await _projectionsService.GetCatalogsDetailsWithPaginationAsync(
                paging: new Paging { Limit = context.Message.Limit, Offset = context.Message.Offset },
                predicate: _ => true,
                cancellationToken: context.CancellationToken);

            await context.RespondAsync<AccountsDetailsPagedResult>(paginatedResult);
        }
    }
}