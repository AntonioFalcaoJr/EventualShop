using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventSourcing.EventStore;

public class UserEventStoreRepository : EventStoreRepository<User, UserStoreEvent, UserSnapshot, Guid>, IUserEventStoreRepository
{
    public UserEventStoreRepository(DbContext dbContext)
        : base(dbContext) { }
}