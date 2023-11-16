using Contracts.Abstractions.Messages;

namespace Contracts.Boundaries.Shopping.Checkout;

public static class Command
{
    public record AddBillingAddress(string CheckoutId, string City, string Complement, string Country, string Number, string State, string Street, string ZipCode) : Message, ICommand;

    public record AddCreditCard(string CheckoutId, string ExpirationDate, string Number, string HolderName, string Cvv) : Message, ICommand;

    public record AddDebitCard(string CheckoutId, string ExpirationDate, string Number, string HolderName, string Cvv) : Message, ICommand;

    public record AddPayPal(string CheckoutId, string Email, string Password) : Message, ICommand;

    public record AddShippingAddress(string CheckoutId, string City, string Complement, string Country, string Number, string State, string Street, string ZipCode) : Message, ICommand;
}