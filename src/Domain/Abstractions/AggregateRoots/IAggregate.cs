using System.Collections.Generic;
using Domain.Abstractions.Entities;

namespace Domain.Abstractions.AggregateRoots
{
    public interface IAggregate<TDomainEvent, out TId> : IEntity<TId>
        where TId : struct
    {
        IReadOnlyCollection<TDomainEvent> Events { get; }
        void Load(IEnumerable<TDomainEvent> events);
    }
}