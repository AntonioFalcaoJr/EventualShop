using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Customers.Projections;
using MassTransit;
using Messages.Customers.Queries;
using Messages.Customers.Queries.Responses;

namespace Application.UseCases.Customers.Queries
{
    public class GetCustomersDetailsWithPaginationConsumer : IConsumer<GetCustomersDetailsWithPagination>
    {
        private readonly ICustomerProjectionsService _projectionsService;

        public GetCustomersDetailsWithPaginationConsumer(ICustomerProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<GetCustomersDetailsWithPagination> context)
        {
            var paginatedResult = await _projectionsService.GetCustomersDetailsWithPaginationAsync(
                paging: new Paging { Limit = context.Message.Limit, Offset = context.Message.Offset },
                predicate: _ => true,
                cancellationToken: context.CancellationToken);

            await context.RespondAsync<CustomersDetailsPagedResult>(paginatedResult);
        }
    }
}