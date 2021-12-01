using System;
using Domain.Abstractions.Entities;

namespace Domain.Entities.PaymentMethods;

public abstract class PaymentMethod : Entity<Guid>, IPaymentMethod
{
    protected PaymentMethod()
    {
        Id = Guid.NewGuid();
    }

    public bool Authorized { get; private set; }
    public decimal Amount { get; init; }

    public void Authorize()
        => Authorized = true;
}