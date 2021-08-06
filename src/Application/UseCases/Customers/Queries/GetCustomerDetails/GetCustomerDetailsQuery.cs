using System;
using System.Threading.Tasks;
using Application.Abstractions.UseCases;
using Application.EventSourcing.Customers.Projections;
using MassTransit;

namespace Application.UseCases.Customers.Queries.GetCustomerDetails
{
    public record GetCustomerDetailsQuery(Guid Id) : IQuery;

    public class GetCustomerDetailQueryHandler : IConsumer<GetCustomerDetailsQuery>
    {
        private readonly ICustomerProjectionsService _projectionsService;

        public GetCustomerDetailQueryHandler(ICustomerProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<GetCustomerDetailsQuery> context)
        {
            var customerId = context.Message.Id;
            var customerDetails = await _projectionsService.GetCustomerDetailsAsync(customerId, context.CancellationToken);
            await context.RespondAsync(customerDetails);
        }
    }
}