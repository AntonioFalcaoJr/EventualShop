using System;
using System.Threading.Tasks;
using Application.Abstractions.Commands;
using MassTransit;

namespace Application.UseCases.Customers.Commands.DeleteCustomer
{
    public record DeleteCustomerCommand(Guid Id) : ICommand;
    
    public class DeleteCustomerCommandConsumer : IConsumer<DeleteCustomerCommand>
    {
        public Task Consume(ConsumeContext<DeleteCustomerCommand> context)
            => throw new System.NotImplementedException();
    }
}