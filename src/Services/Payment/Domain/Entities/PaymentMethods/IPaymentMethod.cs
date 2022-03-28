using Domain.Abstractions.Entities;
using Domain.Enumerations;

namespace Domain.Entities.PaymentMethods;

public interface IPaymentMethod : IEntity<Guid>
{
    decimal Amount { get; }
    PaymentMethodStatus Status { get; }

    void Authorize();
    void Deny();
}