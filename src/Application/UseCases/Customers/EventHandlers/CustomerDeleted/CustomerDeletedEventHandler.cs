using System.Threading.Tasks;
using Domain.Entities.Customers;
using MassTransit;

namespace Application.UseCases.Customers.EventHandlers.CustomerDeleted
{
    public class CustomerDeletedEventHandler : IConsumer<Events.CustomerDeleted>
    {
        public Task Consume(ConsumeContext<Events.CustomerDeleted> context) 
            => throw new System.NotImplementedException();
    }
}