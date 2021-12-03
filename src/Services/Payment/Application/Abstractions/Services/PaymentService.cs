using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.PaymentMethods;

namespace Application.Abstractions.Services;

public abstract class PaymentService : IPaymentService
{
    private IPaymentService _next;

    public IPaymentService SetNext(IPaymentService next)
        => _next = next;

    public virtual Task<IPaymentResult> HandleAsync(IPaymentMethod method, CancellationToken cancellationToken)
        => _next?.HandleAsync(method, cancellationToken);

    protected abstract Task<IPaymentResult> AuthorizeAsync(IPaymentMethod method, CancellationToken cancellationToken);
}