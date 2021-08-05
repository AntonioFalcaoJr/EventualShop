using System.Collections.Generic;
using Domain.Abstractions.Entities;
using Domain.Abstractions.Events;
using Newtonsoft.Json;

namespace Domain.Abstractions.Aggregates
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
        where TId : struct
    {
        [JsonIgnore]
        private readonly List<IDomainEvent> _domainEvents = new();

        [JsonIgnore]
        public IEnumerable<IDomainEvent> DomainEvents
            => _domainEvents;

        public void LoadEvents(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents) 
                ApplyEvent((dynamic)domainEvent);
        }

        private void AddDomainEvent(IDomainEvent domainEvent)
            => _domainEvents.Add(domainEvent);

        protected abstract void ApplyEvent(IDomainEvent domainEvent);

        protected void RaiseEvent(IDomainEvent domainEvent)
        {
            ApplyEvent((dynamic)domainEvent);
            if (IsValid is false) return;
            AddDomainEvent(domainEvent);
        }
    }
}