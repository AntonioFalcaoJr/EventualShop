using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.PaymentMethods;

namespace Application.Abstractions.Services;

public abstract class PaymentService : IPaymentService
{
    protected IPaymentService Next;

    public IPaymentService SetNext(IPaymentService next)
    {
        Next = next;
        return Next;
    }

    public abstract Task<IPaymentResult> HandleAsync(IPaymentMethod paymentMethod, CancellationToken cancellationToken);
    protected abstract Task<IPaymentResult> AuthorizeAsync(IPaymentMethod method, CancellationToken cancellationToken);
}