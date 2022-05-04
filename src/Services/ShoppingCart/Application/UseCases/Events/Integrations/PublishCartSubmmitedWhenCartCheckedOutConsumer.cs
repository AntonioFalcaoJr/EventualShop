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
        var shoppingCart = await _eventStore.LoadAggregateAsync(context.Message.CartId, context.CancellationToken);

        var cartSubmittedEvent = new IntegrationEvent.CartSubmitted(shoppingCart.Id, shoppingCart.Customer, shoppingCart.Total, (IEnumerable<Dto.CartItem>) shoppingCart.Items, (IEnumerable<Dto.IPaymentMethod>)shoppingCart.PaymentMethods);
        
        // TODO - Finish this 
        // var cartSubmittedEvent = new IntegrationEvent.CartSubmitted(shoppingCart.Id, shoppingCart.Customer,
        // ShoppingCartItems: shoppingCart.Items.Select(item => (Dto.CartItem) item),
        //     Total: shoppingCart.Total,
        //     PaymentMethods: shoppingCart.PaymentMethods.Select<IPaymentMethod, Dto.IPaymentMethod>(method
        //         => method switch
        //         {
        //             CreditCard creditCard
        //                 => new Dto.CreditCard
        //                 {
        //                     Amount = creditCard.Amount,
        //                     Expiration = creditCard.Expiration,
        //                     Number = creditCard.Number,
        //                     HolderName = creditCard.HolderName,
        //                     SecurityNumber = creditCard.SecurityNumber
        //                 },
        //             DebitCardPaymentMethod debitCard
        //                 => new Dto.DebitCard
        //                 {
        //                     Amount = debitCard.Amount,
        //                     Expiration = debitCard.Expiration,
        //                     Number = debitCard.Number,
        //                     HolderName = debitCard.HolderName,
        //                     SecurityNumber = debitCard.SecurityNumber
        //                 },
        //             PayPalPaymentMethod payPal
        //                 => new Dto.PayPal
        //                 {
        //                     Amount = payPal.Amount,
        //                     Password = payPal.Password,
        //                     UserName = payPal.UserName
        //                 },
        //             _ => default
        //         }));


        await context.Publish(cartSubmittedEvent, context.CancellationToken);
    }
}