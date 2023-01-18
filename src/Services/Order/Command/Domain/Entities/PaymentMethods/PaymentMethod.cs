using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.PaymentOptions;
using Domain.ValueObjects.PaymentOptions.PayPals;
using Domain.ValueObjects.PaymentOptions.CreditCards;
using Domain.ValueObjects.PaymentOptions.DebitCards;

namespace Domain.Entities.PaymentMethods;

public class PaymentMethod : Entity<PaymentMethodValidator>
{
    public PaymentMethod(Guid id, Money amount, IPaymentOption option)
    {
        Id = id;
        Amount = amount;
        Option = option;
    }

    public Money Amount { get; }
    public IPaymentOption Option { get; }

    public static implicit operator PaymentMethod(Dto.PaymentMethod method)
        => new(method.Id, method.Amount, method.Option switch
        {
            Dto.CreditCard creditCard => (CreditCard)creditCard,
            Dto.DebitCard debitCard => (DebitCard)debitCard,
            Dto.PayPal payPal => (PayPal)payPal,
            _ => throw new NotImplementedException()
        });

    public static implicit operator Dto.PaymentMethod(PaymentMethod method)
        => new(method.Id, method.Amount, method.Option switch
        {
            CreditCard creditCard => (Dto.CreditCard)creditCard,
            DebitCard debitCard => (Dto.DebitCard)debitCard,
            PayPal payPal => (Dto.PayPal)payPal,
            _ => throw new NotImplementedException()
        });
};