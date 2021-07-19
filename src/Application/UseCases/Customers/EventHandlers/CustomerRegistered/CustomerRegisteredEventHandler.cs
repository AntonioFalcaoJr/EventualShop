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
            // _logger.LogError("KKKKKKKKKKKKKKKKKKKKK {Name}", name);
            // await Task.CompletedTask;
            
            // Billing
            // DTO - ID, Nome, Card
            
            // Delivery
            // DTO - Nome, Address
            
            // Transaction{}
            // _repository.Add(DTO);
            // _repository.Add(DTO);
            // _repository.List.Update(DTO); // Catalog
        }
    }
}