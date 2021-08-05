using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Models;
using Application.Abstractions.EventSourcing.Repositories;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions.EventSourcing.EventStore
{
    public abstract class EventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId> : IEventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId>
        where TAggregate : IAggregateRoot<TId>, new()
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

        public async Task<int> AppendEventToStreamAsync(TStoreEvent storeEvent, CancellationToken cancellationToken)
        {
            var entry = await _storeEvents.AddAsync(storeEvent, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entry.Entity.Version;
        }

        public async Task AppendSnapshotToStreamAsync(TSnapshot snapshot, CancellationToken cancellationToken)
        {
            await _snapshots.AddAsync(snapshot, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<IDomainEvent>> GetStreamAsync(TId aggregateId, int snapshotVersion, CancellationToken cancellationToken)
            => await _storeEvents
                .Where(storeEvent => Equals(storeEvent.AggregateId, aggregateId))
                .Where(storeEvent => storeEvent.Version > snapshotVersion)
                .Select(storeEvent => storeEvent.DomainEvent)
                .ToListAsync(cancellationToken);

        public async Task<TSnapshot> GetSnapshotAsync(TId aggregateId, CancellationToken cancellationToken)
            => await _snapshots
                .Where(snapshot => Equals(snapshot.AggregateId, aggregateId))
                .OrderBy(snapshot => snapshot.AggregateVersion)
                .LastOrDefaultAsync(cancellationToken);
    }
}