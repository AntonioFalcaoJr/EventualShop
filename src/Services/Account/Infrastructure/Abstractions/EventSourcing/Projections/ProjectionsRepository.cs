using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Infrastructure.Abstractions.EventSourcing.Projections.Pagination;
using Infrastructure.EventSourcing.Accounts.Projections.Contexts;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Abstractions.EventSourcing.Projections
{
    public abstract class ProjectionsRepository : IProjectionsRepository
    {
        private readonly IMongoDbContext _context;

        protected ProjectionsRepository(IMongoDbContext context)
        {
            _context = context;
        }

        public virtual Task<TProjection> GetAsync<TProjection, TId>(TId id, CancellationToken cancellationToken) where TProjection : IProjection
            => FindAsync<TProjection>(projection => projection.Id.Equals(id), cancellationToken);

        public virtual Task<TProjection> FindAsync<TProjection>(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken) where TProjection : IProjection
            => _context.GetCollection<TProjection>().AsQueryable().Where(predicate).FirstOrDefaultAsync(cancellationToken);

        public virtual Task<IPagedResult<TProjection>> GetAllAsync<TProjection>(Paging paging, Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken) where TProjection : IProjection
        {
            var queryable = _context.GetCollection<TProjection>().AsQueryable().Where(predicate);
            return PagedResult<TProjection>.CreateAsync(paging, queryable, cancellationToken);
        }

        public virtual Task SaveAsync<TProjection>(TProjection projection, CancellationToken cancellationToken) where TProjection : IProjection
            => _context.GetCollection<TProjection>().InsertOneAsync(projection, cancellationToken: cancellationToken);

        public virtual Task SaveManyAsync<TProjection>(IEnumerable<TProjection> projections, CancellationToken cancellationToken)
            where TProjection : IProjection
            => _context
                .GetCollection<TProjection>()
                .InsertManyAsync(projections, cancellationToken: cancellationToken);

        public virtual Task UpdateAsync<TProjection>(TProjection replacement, CancellationToken cancellationToken)
            where TProjection : IProjection
            => _context
                .GetCollection<TProjection>()
                .ReplaceOneAsync(projection
                    => projection.Id.Equals(replacement.Id), replacement, default(ReplaceOptions), cancellationToken);

        public virtual Task UpdateManyAsync<TProjection>(IEnumerable<TProjection> projections, CancellationToken cancellationToken) where TProjection : IProjection
            => throw new NotImplementedException();

        public virtual Task DeleteAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken) where TProjection : IProjection
            => _context.GetCollection<TProjection>().DeleteOneAsync(filter, cancellationToken);

        public virtual Task DeleteManyAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken) where TProjection : IProjection
            => _context.GetCollection<TProjection>().DeleteManyAsync(filter, cancellationToken);
    }
}