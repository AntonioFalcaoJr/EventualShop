using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Messages.Accounts.Commands;

namespace Application.UseCases.Commands
{
    public class DeleteAccountConsumer : IConsumer<DeleteAccount>
    {
        public Task Consume(ConsumeContext<DeleteAccount> context)
            => throw new NotImplementedException();
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