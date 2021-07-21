using System.Threading.Tasks;
using Domain.Entities.Customers;
using MassTransit;

namespace Application.UseCases.Customers.EventHandlers.CustomerUpdated
{
    public class CustomerUpdatedEventConsumer : IConsumer<Events.CustomerAgeChanged>, IConsumer<Events.CustomerNameChanged>
    {
        public Task Consume(ConsumeContext<Events.CustomerAgeChanged> context)
            => throw new System.NotImplementedException();

        public Task Consume(ConsumeContext<Events.CustomerNameChanged> context)
            => throw new System.NotImplementedException();
    }
}