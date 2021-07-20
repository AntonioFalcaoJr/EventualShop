using System.Threading.Tasks;
using Application.Interfaces.Customers;
using Domain.Entities.Customers;
using MassTransit;

namespace Application.UseCases.Customers.EventHandlers.CustomerRegistered
{
    public class CustomerRegisteredEventConsumer : IConsumer<Events.CustomerRegistered>
    {
        private readonly ICustomerEventStoreService _eventStoreService;
        private readonly ICustomerProjectionService _projectionService;

        public CustomerRegisteredEventConsumer(
            ICustomerEventStoreService eventStoreService,
            ICustomerProjectionService projectionService)
        {
            _eventStoreService = eventStoreService;
            _projectionService = projectionService;
        }

        public async Task Consume(ConsumeContext<Events.CustomerRegistered> context)
        {
            var (aggregateId, _, _) = context.Message;
            var customer = await _eventStoreService.LoadAggregateFromStreamAsync(aggregateId, context.CancellationToken);
            await _projectionService.ProjectAsync(customer);
        }
    }
}