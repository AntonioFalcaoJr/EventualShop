using Contracts.Abstractions.Messages;
using Contracts.Boundaries.Shopping.Checkout;
using Domain.Abstractions.Aggregates;
using Domain.Aggregates.ShoppingCarts;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.PaymentMethods;
using Version = Domain.ValueObjects.Version;

namespace Domain.Aggregates.Checkouts;

public class Checkout : AggregateRoot<CheckoutId>
{
    public CartId CartId { get; private set; } = CartId.Undefined;
    public IPaymentMethod PaymentMethod { get; private set; } = IPaymentMethod.Undefined;
    public Address ShippingAddress { get; private set; } = Address.Undefined;
    public Address BillingAddress { get; private set; } = Address.Undefined;

    public static Checkout StartCheckout(CartId cartId)
    {
        Checkout checkout = new();
        DomainEvent.CheckoutStarted @event = new(checkout.Id, cartId, Version.Initial);
        checkout.RaiseEvent(@event);
        return checkout;
    }

    public void AddCreditCard(CreditCard card)
    {
        if (PaymentMethod.Equals(card)) return;

        RaiseEvent(new DomainEvent.CreditCardAdded(Id, CartId,
            card.ExpirationDate, card.Number, card.HolderName, card.Cvv, Version.Next));
    }

    public void AddDebitCard(DebitCard card)
    {
        if (PaymentMethod.Equals(card)) return;

        RaiseEvent(new DomainEvent.DebitCardAdded(Id, CartId,
            card.ExpirationDate, card.Number, card.HolderName, card.Cvv, Version.Next));
    }

    public void AddPayPal(PayPal payPal)
    {
        if (PaymentMethod.Equals(payPal)) return;
        RaiseEvent(new DomainEvent.PayPalAdded(Id, CartId, payPal.Email, payPal.Password, Version.Next));
    }

    public void AddBillingAddress(Address address)
    {
        if (BillingAddress == address) return;

        RaiseEvent(new DomainEvent.BillingAddressAdded(Id, CartId, address.City, address.Complement,
            address.Country, address.Number, address.State, address.Street, address.ZipCode, Version.Next));
    }

    public void AddShippingAddress(Address address)
    {
        if (ShippingAddress == address) return;

        RaiseEvent(new DomainEvent.ShippingAddressAdded(Id, CartId, address.City, address.Complement, 
            address.Country, address.Number, address.State, address.Street, address.ZipCode, Version.Next));
    }

    protected override void ApplyEvent(IDomainEvent @event) => When(@event as dynamic);

    private void When(DomainEvent.CreditCardAdded @event)
        => PaymentMethod = new CreditCard(@event.ExpirationDate, @event.Number, @event.HolderName, @event.Cvv);

    private void When(DomainEvent.DebitCardAdded @event)
        => PaymentMethod = new DebitCard(@event.ExpirationDate, @event.Number, @event.HolderName, @event.Cvv);

    private void When(DomainEvent.PayPalAdded @event)
        => PaymentMethod = new PayPal(@event.Email, @event.Password);

    private void When(DomainEvent.BillingAddressAdded @event)
        => BillingAddress = new(@event.City, @event.Complement,
            @event.Country, @event.Number, @event.State, @event.Street, @event.ZipCode);

    private void When(DomainEvent.ShippingAddressAdded @event)
        => ShippingAddress = new(@event.City, @event.Complement,
            @event.Country, @event.Number, @event.State, @event.Street, @event.ZipCode);
}