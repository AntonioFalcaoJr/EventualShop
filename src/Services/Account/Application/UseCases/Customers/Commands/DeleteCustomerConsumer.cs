using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Messages.Customers.Commands;

namespace Application.UseCases.Customers.Commands
{
    public class DeleteCustomerConsumer : IConsumer<DeleteCustomer>
    {
        public Task Consume(ConsumeContext<DeleteCustomer> context)
            => throw new System.NotImplementedException();
    }
    
    public class MyFilterValidation<T> : IPipeSpecification<T> where T : class, PipeContext
    {
        public void Apply(IPipeBuilder<T> builder)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ValidationResult> Validate() => throw new NotImplementedException();
    }
}