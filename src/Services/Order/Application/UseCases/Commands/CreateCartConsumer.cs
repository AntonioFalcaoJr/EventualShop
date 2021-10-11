using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using CreateCartCommand = Messages.ShoppingCarts.Commands.CreateCart;

namespace Application.UseCases.Commands
{
    public class CreateCartConsumer : IConsumer<CreateCartCommand>
    {
        private readonly IOrderEventStoreService _eventStoreService;

        public CreateCartConsumer(IOrderEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<CreateCartCommand> context)
        {
            var cart = new Order();
            cart.Create(context.Message.CustomerId);
            await _eventStoreService.AppendEventsToStreamAsync(cart, context.CancellationToken);
        }
    }
}