using Domain.Entities.PaymentMethods;

namespace Application.Abstractions.Services;

public abstract class PaymentService : IPaymentService
{
    private IPaymentService _next;

    public IPaymentService SetNext(IPaymentService next)
        => _next = next;

    public virtual Task<IPaymentResult>? HandleAsync(Func<IPaymentService, PaymentMethod, CancellationToken, Task<IPaymentResult?>> behaviorProcessor, PaymentMethod method, CancellationToken cancellationToken)
        => _next.HandleAsync(behaviorProcessor, method, cancellationToken);

    public abstract Task<IPaymentResult?> AuthorizeAsync(PaymentMethod method, CancellationToken cancellationToken);
    public abstract Task<IPaymentResult> CancelAsync(PaymentMethod method, CancellationToken cancellationToken);
    public abstract Task<IPaymentResult> RefundAsync(PaymentMethod method, CancellationToken cancellationToken);
}