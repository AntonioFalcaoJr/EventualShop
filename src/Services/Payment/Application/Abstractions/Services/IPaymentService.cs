using Domain.Entities.PaymentMethods;

namespace Application.Abstractions.Services;

public interface IPaymentService
{
    IPaymentService? SetNext(IPaymentService next);
    Task<IPaymentResult>? HandleAsync(Func<IPaymentService, PaymentMethod, CancellationToken, Task<IPaymentResult?>> behaviorProcessor, PaymentMethod method, CancellationToken cancellationToken);
    Task<IPaymentResult>? AuthorizeAsync(PaymentMethod method, CancellationToken cancellationToken);
    Task<IPaymentResult>? CancelAsync(PaymentMethod method, CancellationToken cancellationToken);
    Task<IPaymentResult>? RefundAsync(PaymentMethod method, CancellationToken cancellationToken);
}