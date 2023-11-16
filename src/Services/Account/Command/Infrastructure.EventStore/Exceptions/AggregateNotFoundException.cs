using System.Reflection;

namespace Infrastructure.EventStore.Exceptions;

public class AggregateNotFoundException(Guid aggregateId, MemberInfo aggregateType) : Exception($"{aggregateType.Name} with id {aggregateId} not found");