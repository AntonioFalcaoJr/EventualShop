using System.Threading.Tasks;
using Application.EventSourcing.Customers.EventStore;
using MassTransit;
using Messages.Customers.Commands;

namespace Application.UseCases.Customers.Commands
{
    public class UpdateCustomerConsumer : IConsumer<UpdateCustomer>
    {
        private readonly ICustomerEventStoreService _eventStoreService;

        public UpdateCustomerConsumer(ICustomerEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<UpdateCustomer> context)
        {
            var customer = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.Id, context.CancellationToken);

            customer.ChangeAge(context.Message.Age);
            customer.ChangeName(context.Message.Name);

            await _eventStoreService.AppendEventsToStreamAsync(customer, context.CancellationToken);
        }
    }
}