using Application.EventStore;
using Contracts.DataTransferObjects;
using Contracts.Services.ShoppingCart;
using Domain.Entities.PaymentMethods;
using Domain.ValueObjects.PaymentOptions.CreditCards;
using Domain.ValueObjects.PaymentOptions.DebitCards;
using Domain.ValueObjects.PaymentOptions.PayPals;
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
        var shoppingCart = await _eventStore.LoadAggregateAsync(context.Message.CartId, context.CancellationToken);

        IntegrationEvent.CartSubmitted cartSubmittedEvent = new(
            shoppingCart.Id,
            shoppingCart.Customer,
            shoppingCart.Total,
            shoppingCart.Items.Select(item => (Dto.CartItem) item),
            shoppingCart.PaymentMethods.Select<PaymentMethod, Dto.PaymentMethod>(method
                => method.Option switch
                {
                    CreditCard creditCard => new(method.Id, method.Amount, (Dto.CreditCard) creditCard),
                    DebitCard debitCard => new(method.Id, method.Amount, (Dto.DebitCard) debitCard),
                    PayPal payPal => new(method.Id, method.Amount, (Dto.PayPal) payPal),
                    _ => default
                }));

        await context.Publish(cartSubmittedEvent, context.CancellationToken);
    }
}