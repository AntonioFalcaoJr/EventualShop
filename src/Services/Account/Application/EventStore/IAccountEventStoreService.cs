using Application.Abstractions.EventStore;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IAccountEventStoreService : IEventStoreService<Guid, Account> { }