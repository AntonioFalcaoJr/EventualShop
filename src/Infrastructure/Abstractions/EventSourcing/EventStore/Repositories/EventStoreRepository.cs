using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions.EventSourcing.EventStore.Repositories
{
    public abstract class EventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId> : IEventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId>
        where TAggregate : IAggregate<TId>, new()
        where TStoreEvent : StoreEvent<TAggregate, TId>
        where TSnapshot : Snapshot<TAggregate, TId>
        where TId : struct
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TSnapshot> _snapshots;
        private readonly DbSet<TStoreEvent> _storeEvents;

        protected EventStoreRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _storeEvents = dbContext.Set<TStoreEvent>();
            _snapshots = dbContext.Set<TSnapshot>();
        }

        public async Task<int> AppendEventToStreamAsync(TStoreEvent @event, CancellationToken cancellationToken)
        {
            var entry = await _storeEvents.AddAsync(@event, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entry.Entity.Version;
        }

        public async Task AppendSnapshotToStreamAsync(TSnapshot snapshot, CancellationToken cancellationToken)
        {
            await _snapshots.AddAsync(snapshot, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<IEvent>> GetStreamAsync(TId aggregateId, int snapshotVersion, CancellationToken cancellationToken)
            => await _storeEvents
                .Where(@event => Equals(@event.AggregateId, aggregateId))
                .Where(@event => @event.Version > snapshotVersion)
                .Select(@event => @event.Event)
                .ToListAsync(cancellationToken);

        public async Task<TSnapshot> GetSnapshotAsync(TId aggregateId, CancellationToken cancellationToken)
            => await _snapshots
                .Where(@event => Equals(@event.AggregateId, aggregateId))
                .OrderBy(@event => @event.Version)
                .LastOrDefaultAsync(cancellationToken);
    }
}