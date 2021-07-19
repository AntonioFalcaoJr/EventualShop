using System.Threading.Tasks;
using Domain.Entities.Customers;
using MassTransit;

namespace Application.UseCases.Customers.EventHandlers.CustomerDeleted
{
    public class CustomerDeletedEventHandler : IConsumer<Events.CustomerDeleted>
    {
        public async Task Consume(ConsumeContext<Events.CustomerDeleted> context)
        {
            var id = context.Message;
            await Task.CompletedTask;
        }
    }
}