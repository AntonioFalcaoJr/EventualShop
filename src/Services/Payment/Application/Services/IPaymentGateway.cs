using Domain.Aggregates;

namespace Application.Services;

public interface IPaymentGateway
{
    Task AuthorizeAsync(Payment payment, CancellationToken cancellationToken);
    Task CancelAsync(Payment payment, CancellationToken cancellationToken);
    Task RefundAsync(Payment payment, CancellationToken cancellationToken);
}