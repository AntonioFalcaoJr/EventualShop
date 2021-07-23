using System.Collections.Generic;
using Domain.Abstractions.Entities;
using Domain.Abstractions.Events;

namespace Domain.Abstractions.Aggregates
{
    public interface IAggregateRoot<out TId> : IEntity<TId>
        where TId : struct
    {
        int Version { get; }
        IEnumerable<IDomainEvent> Events { get; }
        void Load(IEnumerable<IDomainEvent> events);
    }
}