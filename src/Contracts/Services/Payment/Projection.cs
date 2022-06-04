using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Payment;

public static class Projection
{
    public record Payment(Guid Id, Guid OrderId, decimal Amount, Dto.Address BillingAddress, IEnumerable<Dto.PaymentMethod> PaymentMethods, string Status, bool IsDeleted) : IProjection;
}