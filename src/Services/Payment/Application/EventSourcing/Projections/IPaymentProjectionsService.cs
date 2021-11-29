using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections;

public interface IPaymentProjectionsService : IProjectionsService
{
    Task<PaymentDetailsProjection> GetPaymentDetailsAsync(Guid paymentId, CancellationToken cancellationToken);
}