using System;
using System.Threading.Tasks;
using Domain.Entities.Customers;
using MassTransit;

namespace Application.UseCases.Customers.EventHandlers.CustomerUpdated
{
    public class CustomerUpdatedConsumer : IConsumer<Events.CustomerAgeChanged>, IConsumer<Events.CustomerNameChanged>
    {
        public Task Consume(ConsumeContext<Events.CustomerAgeChanged> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<Events.CustomerNameChanged> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }
    }
}