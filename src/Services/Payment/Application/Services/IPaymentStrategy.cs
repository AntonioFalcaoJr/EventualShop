using System.Threading;
using System.Threading.Tasks;
using Domain.Aggregates;

namespace Application.Services;

public interface IPaymentStrategy
{
    Task ProceedWithPaymentAsync(Payment payment, CancellationToken cancellationToken);
}