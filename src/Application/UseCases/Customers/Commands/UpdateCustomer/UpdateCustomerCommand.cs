using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Abstractions.Commands;
using Application.Interfaces;
using Application.Interfaces.Customers;
using MassTransit;

namespace Application.UseCases.Customers.Commands.UpdateCustomer
{
    public record UpdateCustomerCommand(Guid Id, string Name, int Age) : ICommand;

    public class UpdateCustomerCommandConsumer : IConsumer<UpdateCustomerCommand>
    {
        private readonly ICustomerEventStoreService _eventStoreService;

        public UpdateCustomerCommandConsumer(ICustomerEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<UpdateCustomerCommand> context)
        {
            var (aggregateId, name, age) = context.Message;

            var customer = await _eventStoreService.LoadAggregateFromStreamAsync(aggregateId, context.CancellationToken);

            customer.ChangeAge(age);
            customer.ChangeName(name);

            await _eventStoreService.AppendEventsToStreamAsync(customer, context.CancellationToken);
        }
    }
}