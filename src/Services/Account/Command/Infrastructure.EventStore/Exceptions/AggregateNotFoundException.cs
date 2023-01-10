using System.Reflection;

namespace Infrastructure.EventStore.Exceptions;

public class AggregateNotFoundException : Exception
{
    public AggregateNotFoundException(Guid aggregateId, MemberInfo aggregateType)
        : base($"{aggregateType.Name} with id {aggregateId} not found") { }
}