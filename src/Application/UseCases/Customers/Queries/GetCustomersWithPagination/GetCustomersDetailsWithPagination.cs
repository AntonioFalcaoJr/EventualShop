using System.Threading.Tasks;
using Application.Abstractions.UseCases;
using Application.EventSourcing.Customers.Projections;
using MassTransit;

namespace Application.UseCases.Customers.Queries.GetCustomersWithPagination
{
    public record GetCustomersDetailsWithPagination(int Limit, int Offset) : IQueryPaging;

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
                paging: context.Message, 
                predicate: _ => true, 
                cancellationToken: context.CancellationToken);
            
            await context.RespondAsync(paginatedResult);
        }
    }
}