using Domain.Abstractions.Entities;
using Domain.Enumerations;
using FluentValidation;

namespace Domain.Entities.PaymentMethods;

public abstract class PaymentMethod<TValidator> : Entity<Guid, TValidator>, IPaymentMethod
    where TValidator : IValidator, new()
{
    protected PaymentMethod(Guid? id, decimal amount)
    {
        Id = id ?? Guid.NewGuid();
        Amount = amount;
        Status = PaymentMethodStatus.Ready;
    }

    public decimal Amount { get; }
    public PaymentMethodStatus Status { get; private set; }

    public void Authorize()
        => Status = PaymentMethodStatus.Authorized;

    public void Deny()
        => Status = PaymentMethodStatus.Denied;
}