using System.Threading.Tasks;
using Application.EventSourcing.Customers.Projections;
using MassTransit;
using Messages.Customers.Queries;

namespace Application.UseCases.Customers.Queries
{
    public class GetCustomerDetailsConsumer : IConsumer<GetCustomerDetails>
    {
        private readonly ICustomerProjectionsService _projectionsService;

        public GetCustomerDetailsConsumer(ICustomerProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<GetCustomerDetails> context)
        {
            var customerDetails = await _projectionsService.GetCustomerDetailsAsync(context.Message.Id, context.CancellationToken);
            await context.RespondAsync(customerDetails);
        }
    }
}