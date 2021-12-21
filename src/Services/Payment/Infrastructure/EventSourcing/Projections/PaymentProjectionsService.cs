using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using Application.EventSourcing.Projections;

namespace Infrastructure.EventSourcing.Projections;

public class PaymentProjectionsService : IPaymentProjectionsService
{
    private readonly IPaymentProjectionsRepository _repository;

    public PaymentProjectionsService(IPaymentProjectionsRepository repository)
    {
        _repository = repository;
    }

    public Task<PaymentDetailsProjection> GetPaymentDetailsAsync(Guid paymentId, CancellationToken cancellationToken)
        => _repository.FindAsync<PaymentDetailsProjection>(projection => projection.Id == paymentId, cancellationToken);

    public Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.UpsertAsync(projection, cancellationToken);

    public Task RemoveAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.DeleteAsync(filter, cancellationToken);
}