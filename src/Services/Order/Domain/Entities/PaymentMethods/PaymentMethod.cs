using Domain.Abstractions.Entities;
using FluentValidation;

namespace Domain.Entities.PaymentMethods;

public abstract class PaymentMethod<TValidator> : Entity<Guid, TValidator>, IPaymentMethod 
    where TValidator : IValidator, new()
{
    public bool Authorized { get; private set; }
    public decimal Amount { get; init; }

    public void Authorize()
        => Authorized = true;
}