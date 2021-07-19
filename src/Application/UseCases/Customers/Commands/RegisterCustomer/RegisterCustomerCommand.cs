using System.Threading.Tasks;
using Application.Abstractions.Commands;
using Application.Interfaces;
using Domain.Entities.Customers;
using MassTransit;

namespace Application.UseCases.Customers.Commands.RegisterCustomer
{
    public record RegisterCustomerCommand(string Name, int Age) : ICommand;
    
    public class RegisterCustomerHandler : IConsumer<RegisterCustomerCommand>
    {
        private readonly ICustomerEventStoreService _eventStoreService;

        public RegisterCustomerHandler(ICustomerEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<RegisterCustomerCommand> context)
        {
            var customer = new Customer();
            var (name, age) = context.Message;
            customer.Register(name, age);
            await _eventStoreService.AppendEventsToStreamAsync(customer, context.CancellationToken);
        }
    }
}