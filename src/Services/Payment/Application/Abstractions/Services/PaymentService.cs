using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.PaymentMethods;

namespace Application.Abstractions.Services;

public abstract class PaymentService : IPaymentService
{
    private IPaymentService _next;

    public IPaymentService SetNext(IPaymentService next)
        => _next = next;

    public virtual Task<IPaymentResult> HandleAsync(Func<IPaymentService, IPaymentMethod, CancellationToken, Task<IPaymentResult>> behaviorProcessor, IPaymentMethod method, CancellationToken cancellationToken)
        => _next?.HandleAsync(behaviorProcessor, method, cancellationToken);

    public abstract Task<IPaymentResult> AuthorizeAsync(IPaymentMethod method, CancellationToken cancellationToken);
    public abstract Task<IPaymentResult> CancelAsync(IPaymentMethod method, CancellationToken cancellationToken);
    public abstract Task<IPaymentResult> RefundAsync(IPaymentMethod method, CancellationToken cancellationToken);
}