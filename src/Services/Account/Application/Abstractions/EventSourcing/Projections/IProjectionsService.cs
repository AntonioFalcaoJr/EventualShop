using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Abstractions.EventSourcing.Projections;

public interface IProjectionsService
{
    Task ProjectNewAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection;

    Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection;

    Task RemoveAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        where TProjection : IProjection;
}