using Contracts.Abstractions.Messages;

namespace Domain.Abstractions.StoreEvents;

public interface IStoreEvent<TId>
    where TId : struct
{
    public long Version { get; }
    public TId AggregateId { get; init; }
    public string DomainEventName { get; init; }
    public IEvent DomainEvent { get; init; }
}