using System.Collections.Generic;
using Domain.Abstractions.Entities;

namespace Domain.Abstractions.Aggregates
{
    public interface IAggregate<TDomainEvent, out TId> : IEntity<TId>
        where TId : struct
    {
        IReadOnlyCollection<TDomainEvent> Events { get; }
        void Load(IEnumerable<TDomainEvent> events);
    }
}