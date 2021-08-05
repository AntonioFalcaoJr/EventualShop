using System.Collections.Generic;
using Domain.Abstractions.Entities;
using Domain.Abstractions.Events;

namespace Domain.Abstractions.Aggregates
{
    public interface IAggregateRoot<out TId> : IEntity<TId>
        where TId : struct
    {
        IEnumerable<IDomainEvent> DomainEvents { get; }
        void LoadEvents(IEnumerable<IDomainEvent> domainEvents);
    }
}