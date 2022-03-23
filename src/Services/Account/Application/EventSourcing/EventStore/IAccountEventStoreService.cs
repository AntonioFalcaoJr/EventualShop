using Application.Abstractions.EventSourcing.EventStore;
using Domain.Aggregates;

namespace Application.EventSourcing.EventStore;

public interface IAccountEventStoreService : IEventStoreService<Account, Guid> { }