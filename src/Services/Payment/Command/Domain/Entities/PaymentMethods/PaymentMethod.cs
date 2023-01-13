using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.Enumerations;
using Domain.ValueObjects;
using Domain.ValueObjects.PaymentOptions;
using Domain.ValueObjects.PaymentOptions.CreditCards;
using Domain.ValueObjects.PaymentOptions.DebitCards;
using Domain.ValueObjects.PaymentOptions.PayPals;

namespace Domain.Entities.PaymentMethods;

public class PaymentMethod : Entity<PaymentMethodValidator>
{
    public PaymentMethod(Guid id, Money amount, IPaymentOption? option)
    {
        Id = id;
        Amount = amount;
        Option = option;
        Status = PaymentMethodStatus.Ready;
    }

    public Money Amount { get; }
    public IPaymentOption? Option { get; }
    public PaymentMethodStatus Status { get; private set; }

    public void Authorize()
        => Status = PaymentMethodStatus.Authorized;

    public void Deny()
        => Status = PaymentMethodStatus.Denied;

    public void Cancel()
        => Status = PaymentMethodStatus.Canceled;

    public void DenyCancellation()
        => Status = PaymentMethodStatus.CancellationDenied;

    public void Refund()
        => Status = PaymentMethodStatus.Refunded;

    public void DenyRefund()
        => Status = PaymentMethodStatus.RefundDenied;

    public static implicit operator PaymentMethod(Dto.PaymentMethod method)
        => new(method.Id, method.Amount, method.Option switch
        {
            Dto.CreditCard creditCard => (CreditCard) creditCard,
            Dto.DebitCard debitCard => (DebitCard) debitCard,
            Dto.PayPal payPal => (PayPal) payPal,
            _ => default
        });

    public static implicit operator Dto.PaymentMethod(PaymentMethod method)
        => new(method.Id, (method.Amount.Value, method.Amount.Currency.Name), method.Option switch
        {
            CreditCard creditCard => (Dto.CreditCard) creditCard,
            DebitCard debitCard => (Dto.DebitCard) debitCard,
            PayPal payPal => (Dto.PayPal) payPal,
            _ => default
        });
}