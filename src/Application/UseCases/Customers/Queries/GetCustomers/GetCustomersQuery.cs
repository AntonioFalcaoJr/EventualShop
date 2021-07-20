using System.Threading.Tasks;
using Application.Abstractions;
using Application.Abstractions.Queries;
using Application.Interfaces;
using Application.Interfaces.Customers;
using MassTransit;

namespace Application.UseCases.Customers.Queries.GetCustomers
{
    public record GetCustomersQuery : IQuery;

    public class GetCustomersQueryHandler : IConsumer<GetCustomersQuery>
    {
        private readonly ICustomerProjectionService _projectionService;

        public GetCustomersQueryHandler(ICustomerProjectionService projectionService)
        {
            _projectionService = projectionService;
        }

        public async Task Consume(ConsumeContext<GetCustomersQuery> context)
        {
            var customers = await _projectionService.GetCustomersAsync();
            await context.RespondAsync<Models.GetCustomersModel>(new(customers));
        }
    }
}