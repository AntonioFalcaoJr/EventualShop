using System.Collections.Generic;
using Domain.Abstractions.Entities;
using Domain.Abstractions.Events;

namespace Domain.Abstractions.Aggregates
{
    public interface IAggregate<out TId> : IEntity<TId>
        where TId : struct
    {
        IEnumerable<IEvent> Events { get; }
        void Load(IEnumerable<IEvent> events);
    }
}