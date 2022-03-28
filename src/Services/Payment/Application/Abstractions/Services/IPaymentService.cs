using Domain.Entities.PaymentMethods;

namespace Application.Abstractions.Services;

public interface IPaymentService
{
    IPaymentService SetNext(IPaymentService next);
    Task<IPaymentResult> HandleAsync(Func<IPaymentService, IPaymentMethod, CancellationToken,Task<IPaymentResult>> behaviorProcessor, IPaymentMethod method, CancellationToken cancellationToken);
    Task<IPaymentResult> AuthorizeAsync(IPaymentMethod method, CancellationToken cancellationToken);
    Task<IPaymentResult> CancelAsync(IPaymentMethod method, CancellationToken cancellationToken);
    Task<IPaymentResult> RefundAsync(IPaymentMethod method, CancellationToken cancellationToken);
}