using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.EventSourcing.Projections
{
    public interface IPaymentProjectionsService
    {
        Task ProjectPaymentDetailsAsync(PaymentDetailsProjection paymentDetails, CancellationToken cancellationToken);
        Task UpdatePaymentDetailsAsync(PaymentDetailsProjection paymentDetails, CancellationToken cancellationToken);
        Task<PaymentDetailsProjection> GetPaymentDetailsAsync(Guid paymentId, CancellationToken cancellationToken);
    }
}