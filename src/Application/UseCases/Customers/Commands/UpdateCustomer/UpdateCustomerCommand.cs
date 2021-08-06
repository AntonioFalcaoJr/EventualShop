using System;
using System.Threading.Tasks;
using Application.Abstractions.UseCases;
using Application.EventSourcing.Customers.EventStore;
using MassTransit;

namespace Application.UseCases.Customers.Commands.UpdateCustomer
{
    public record UpdateCustomerCommand(Guid Id, string Name, int Age) : ICommand;

    public class UpdateCustomerCommandHandler : IConsumer<UpdateCustomerCommand>
    {
        private readonly ICustomerEventStoreService _eventStoreService;

        public UpdateCustomerCommandHandler(ICustomerEventStoreService eventStoreService)
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