using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using CreateCartCommand = Messages.ShoppingCarts.Commands.CreateCart;

namespace Application.UseCases.Commands
{
    public class CreateCartConsumer : IConsumer<CreateCartCommand>
    {
        private readonly IShoppingCartEventStoreService _eventStoreService;

        public CreateCartConsumer(IShoppingCartEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<CreateCartCommand> context)
        {
            var cart = new Cart();
            cart.Handle(context.Message);
            await _eventStoreService.AppendEventsToStreamAsync(cart, context.CancellationToken);
        }
    }
}