using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;

namespace Application.Abstractions.EventSourcing.Projections
{
    public interface IProjectionsRepository
    {
        Task<TProjection> FindAsync<TProjection>(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken)
            where TProjection : IProjection;

        Task<TProjection> GetAsync<TProjection, TId>(TId id, CancellationToken cancellationToken)
            where TProjection : IProjection;

        Task<IPagedResult<TProjection>> GetAllAsync<TProjection>(Paging paging, Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken)
            where TProjection : IProjection;

        Task SaveAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
            where TProjection : IProjection;

        Task DeleteAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
            where TProjection : IProjection;
    }
}