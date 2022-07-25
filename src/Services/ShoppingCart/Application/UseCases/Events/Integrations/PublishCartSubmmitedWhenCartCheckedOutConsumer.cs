using Application.EventStore;
using Contracts.DataTransferObjects;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Events.Integrations;

public class PublishCartSubmittedWhenCheckedOutConsumer : IConsumer<DomainEvent.CartCheckedOut>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public PublishCartSubmittedWhenCheckedOutConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<DomainEvent.CartCheckedOut> context)
    {
        var shoppingCart = await _eventStore.LoadAsync(context.Message.CartId, context.CancellationToken);

        IntegrationEvent.CartSubmitted cartSubmittedEvent = new(
            shoppingCart.Id,
            shoppingCart.CustomerId,
            shoppingCart.Total,
            shoppingCart.BillingAddress,
            shoppingCart.ShippingAddress,
            shoppingCart.Items.Select(item => (Dto.CartItem) item),
            shoppingCart.PaymentMethods.Select(method => (Dto.PaymentMethod) method));

        await context.Publish(cartSubmittedEvent, context.CancellationToken);
    }
}