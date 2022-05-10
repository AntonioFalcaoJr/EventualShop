using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.ValueObjects.PaymentOptions;
using Domain.ValueObjects.PaymentOptions.PayPals;
using Domain.ValueObjects.PaymentOptions.CreditCards;
using Domain.ValueObjects.PaymentOptions.DebitCards;

namespace Domain.Entities.PaymentMethods;

public class PaymentMethod : Entity<Guid, PaymentMethodValidator>
{
    public PaymentMethod(Guid id, decimal amount, IPaymentOption option)
    {
        Id = id;
        Amount = amount;
        Option = option;
    }

    public decimal Amount { get; }
    public IPaymentOption Option { get; }

    public static implicit operator PaymentMethod(Dto.PaymentMethod method)
        => new(method.Id, method.Amount, method.Option switch
        {
            Dto.CreditCard creditCard => (CreditCard) creditCard,
            Dto.DebitCard debitCard => (DebitCard) debitCard,
            Dto.PayPal payPal => (PayPal) payPal,
            _ => default
        });

    public static implicit operator Dto.PaymentMethod(PaymentMethod method)
        => new(method.Id, method.Amount, method.Option switch
        {
            CreditCard creditCard => (Dto.CreditCard) creditCard,
            DebitCard debitCard => (Dto.DebitCard) debitCard,
            PayPal payPal => (Dto.PayPal) payPal,
            _ => default
        });
};