using System;
using System.Threading.Tasks;
using Domain.Entities.Accounts;
using MassTransit;

namespace Application.UseCases.EventHandlers
{
    public class AccountUpdatedConsumer : IConsumer<Events.AccountAgeChanged>, IConsumer<Events.AccountNameChanged>
    {
        public Task Consume(ConsumeContext<Events.AccountAgeChanged> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<Events.AccountNameChanged> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }
    }
}