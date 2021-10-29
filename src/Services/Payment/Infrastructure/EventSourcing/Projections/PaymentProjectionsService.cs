using System;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.Projections;

namespace Infrastructure.EventSourcing.Projections;

public class PaymentProjectionsService : IPaymentProjectionsService
{
    private readonly IPaymentProjectionsRepository _repository;

    public PaymentProjectionsService(IPaymentProjectionsRepository repository)
    {
        _repository = repository;
    }

    public Task ProjectPaymentDetailsAsync(PaymentDetailsProjection paymentDetails, CancellationToken cancellationToken)
        => _repository.SaveAsync(paymentDetails, cancellationToken);
        
    public Task UpdatePaymentDetailsAsync(PaymentDetailsProjection paymentDetails, CancellationToken cancellationToken)
        => _repository.UpsertAsync(paymentDetails, cancellationToken);

    public Task<PaymentDetailsProjection> GetPaymentDetailsAsync(Guid paymentId, CancellationToken cancellationToken)
        => _repository.FindAsync<PaymentDetailsProjection>(projection => projection.Id == paymentId, cancellationToken);
}