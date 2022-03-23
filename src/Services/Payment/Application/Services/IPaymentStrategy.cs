using Domain.Aggregates;

namespace Application.Services;

public interface IPaymentStrategy
{
    Task AuthorizePaymentAsync(Payment payment, CancellationToken cancellationToken);
    Task CancelPaymentAsync(Payment payment, CancellationToken cancellationToken);
    Task RefundPaymentAsync(Payment payment, CancellationToken cancellationToken);
}