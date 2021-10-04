using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Accounts;
using GetAccountsDetailsWithPaginationQuery = Messages.Accounts.Queries.GetAccountsDetailsWithPagination;

namespace Application.UseCases.Queries
{
    public class GetAccountsDetailsWithPaginationConsumer : IConsumer<GetAccountsDetailsWithPaginationQuery>
    {
        private readonly IAccountProjectionsService _projectionsService;

        public GetAccountsDetailsWithPaginationConsumer(IAccountProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<GetAccountsDetailsWithPaginationQuery> context)
        {
            var paginatedResult = await _projectionsService.GetAccountsDetailsWithPaginationAsync(
                paging: new Paging { Limit = context.Message.Limit, Offset = context.Message.Offset },
                predicate: _ => true,
                cancellationToken: context.CancellationToken);

            await context.RespondAsync<Responses.AccountsDetailsPagedResult>(paginatedResult);
        }
    }
}