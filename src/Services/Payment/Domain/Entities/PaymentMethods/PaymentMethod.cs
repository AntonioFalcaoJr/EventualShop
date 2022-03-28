using Domain.Abstractions.Entities;
using Domain.Enumerations;

namespace Domain.Entities.PaymentMethods;

public abstract class PaymentMethod : Entity<Guid>, IPaymentMethod
{
    protected PaymentMethod(Guid id, decimal amount)
    {
        Id = id;
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