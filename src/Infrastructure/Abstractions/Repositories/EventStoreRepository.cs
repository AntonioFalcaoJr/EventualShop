using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions.Repositories
{
    public abstract class EventStoreRepository<TAggregate, TStoreEvent, TId> : IEventStoreRepository<TAggregate, TStoreEvent, TId>
        where TAggregate : IAggregate<TId>, new()
        where TStoreEvent : StoreEvent<TAggregate, TId>, new()
        where TId : struct
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TStoreEvent> _dbSet;

        protected EventStoreRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TStoreEvent>();
        }

        public async Task AppendEventsToStreamAsync(IEnumerable<TStoreEvent> events, CancellationToken cancellationToken)
        {
            await _dbSet.AddRangeAsync(events, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<IEvent>> GetStreamAsync(TId aggregateId, CancellationToken cancellationToken)
            => await _dbSet
                .Where(@event => Equals(@event.AggregateId, aggregateId))
                .Select(@event => @event.Event)
                .ToListAsync(cancellationToken);
    }
}