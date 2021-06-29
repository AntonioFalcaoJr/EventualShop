using System.Collections.Generic;
using Domain.Abstractions.Events;

namespace Domain.Abstractions.AggregateRoots
{
    public interface IAggregateRoot
    {
        void RaiseEvent(IEvent @event);
        IEnumerable<IEvent> GetEvents();
        void ClearEvents();
    }
}