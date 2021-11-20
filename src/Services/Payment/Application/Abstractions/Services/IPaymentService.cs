using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.PaymentMethods;

namespace Application.Abstractions.Services;

public interface IPaymentService
{
    IPaymentService SetNext(IPaymentService next);
    Task<IPaymentResult> HandleAsync(IPaymentMethod paymentMethod, CancellationToken cancellationToken);
}