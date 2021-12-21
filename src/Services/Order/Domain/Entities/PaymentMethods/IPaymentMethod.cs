using System;
using Domain.Abstractions.Entities;

namespace Domain.Entities.PaymentMethods;

public interface IPaymentMethod : IEntity<Guid>
{
    bool Authorized { get; }
    decimal Amount { get; }

    void Authorize();
}