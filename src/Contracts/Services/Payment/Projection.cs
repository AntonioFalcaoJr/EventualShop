using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Payment;

public static class Projection
{
    public record Payment(Guid Id, Guid OrderId, decimal Amount, Dto.Address BillingAddress, IEnumerable<Dto.PaymentMethod> PaymentMethods, string Status, bool IsDeleted) : IProjection;

    public record PaymentMethod(Guid Id, Guid PaymentId, decimal Amount, Dto.IPaymentOption Option, string Status, bool IsDeleted) : IProjection;
}