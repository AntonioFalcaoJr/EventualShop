using System.Threading.Tasks;
using Domain.Entities.Customers;
using MassTransit;

namespace Application.UseCases.Customers.EventHandlers.CustomerUpdated
{
    public class CustomerUpdatedEventHandler
    {
        private class CustomerAgeChangedEventHandler : IConsumer<Events.CustomerAgeChanged>
        {
            public async Task Consume(ConsumeContext<Events.CustomerAgeChanged> context)
            {
                var age = context.Message;
                await Task.CompletedTask;
            }
        }

        private class CustomerNameChangedEventHandler : IConsumer<Events.CustomerNameChanged>
        {
            public async Task Consume(ConsumeContext<Events.CustomerNameChanged> context)
            {
                var name = context.Message;
                await Task.CompletedTask;
            }
        }
    }
}