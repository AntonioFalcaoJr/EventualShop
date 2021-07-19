using System.Threading.Tasks;
using Domain.Entities.Customers;
using MassTransit;

namespace Application.UseCases.Customers.EventHandlers.CustomerRegistered
{
    public class CustomerRegisteredEventHandler : IConsumer<Events.CustomerRegistered>
    {
        public async Task Consume(ConsumeContext<Events.CustomerRegistered> context)
        {
            var (id, name, age) = context.Message;
            await Task.CompletedTask;
        }
    }
}