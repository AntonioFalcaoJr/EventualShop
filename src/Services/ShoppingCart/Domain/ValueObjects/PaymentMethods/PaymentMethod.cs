using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.PaymentMethods;

public abstract record PaymentMethod(decimal Amount) : ValueObject, IPaymentMethod;