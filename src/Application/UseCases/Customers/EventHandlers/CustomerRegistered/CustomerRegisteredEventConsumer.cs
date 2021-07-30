using System.Threading.Tasks;
using Application.EventSourcing.Customers.EventStore;
using Application.EventSourcing.Customers.Projections;
using Application.UseCases.Customers.Queries.GetCustomers;
using Domain.Entities.Customers;
using MassTransit;

namespace Application.UseCases.Customers.EventHandlers.CustomerRegistered
{
    public class CustomerRegisteredEventConsumer : IConsumer<Events.CustomerRegistered>
    {
        private readonly ICustomerEventStoreService _eventStoreService;
        private readonly ICustomerProjectionsService _projectionsService;

        public CustomerRegisteredEventConsumer(
            ICustomerEventStoreService eventStoreService,
            ICustomerProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<Events.CustomerRegistered> context)
        {
            var (aggregateId, _, _) = context.Message;
            
            var customer = await _eventStoreService.LoadAggregateFromStreamAsync(aggregateId, context.CancellationToken);

            var customerDetails = new Models.CustomerDetails
            {
                Id = customer.Id,
                Age = customer.Age,
                Name = customer.Name
            };

            await _projectionsService.ProjectNewCustomerDetailsAsync(customerDetails, context.CancellationToken);
        }
    }
}