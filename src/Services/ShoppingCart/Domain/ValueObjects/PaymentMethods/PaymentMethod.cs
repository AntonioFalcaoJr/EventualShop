using Domain.Abstractions.ValueObjects;
using FluentValidation;

namespace Domain.ValueObjects.PaymentMethods;

public abstract record PaymentMethod<TValidator>(decimal Amount) : ValueObject<TValidator>, IPaymentMethod 
    where TValidator : IValidator, new();