using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Accounts;

namespace Application.UseCases.Queries
{
    public class GetAccountsDetailsWithPaginationConsumer : IConsumer<Messages.Accounts.Queries.GetAccountsDetailsWithPagination>
    {
        private readonly IAccountProjectionsService _projectionsService;

        public GetAccountsDetailsWithPaginationConsumer(IAccountProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<Messages.Accounts.Queries.GetAccountsDetailsWithPagination> context)
        {
            var paginatedResult = await _projectionsService.GetAccountsDetailsWithPaginationAsync(
                paging: new Paging { Limit = context.Message.Limit, Offset = context.Message.Offset },
                predicate: _ => true,
                cancellationToken: context.CancellationToken);

            await context.RespondAsync<Responses.AccountsDetailsPagedResult>(paginatedResult);
        }
    }
}