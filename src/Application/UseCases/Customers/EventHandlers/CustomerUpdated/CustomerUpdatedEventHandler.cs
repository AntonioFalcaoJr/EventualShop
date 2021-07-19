using System.Threading.Tasks;
using Domain.Entities.Customers;
using MassTransit;

namespace Application.UseCases.Customers.EventHandlers.CustomerUpdated
{
    public class CustomerUpdatedEventHandler
    {
        private class CustomerAgeChangedEventHandler : IConsumer<Events.CustomerAgeChanged>
        {
            public Task Consume(ConsumeContext<Events.CustomerAgeChanged> context)
                => throw new System.NotImplementedException();
        }

        private class CustomerNameChangedEventHandler : IConsumer<Events.CustomerNameChanged>
        {
            public Task Consume(ConsumeContext<Events.CustomerNameChanged> context)
                => throw new System.NotImplementedException();
        }
    }
}