using System.Threading.Tasks;
using Application.EventSourcing.Customers.EventStore;
using Domain.Entities.Customers;
using MassTransit;
using Messages.Customers.Commands;

namespace Application.UseCases.Customers.Commands
{
    public class RegisterCustomerConsumer : IConsumer<RegisterCustomer>
    {
        private readonly ICustomerEventStoreService _eventStoreService;

        public RegisterCustomerConsumer(ICustomerEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<RegisterCustomer> context)
        {
            var customer = new Customer();
            customer.Register(context.Message.Name, context.Message.Age);
            await _eventStoreService.AppendEventsToStreamAsync(customer, context.CancellationToken);
        }
    }
}