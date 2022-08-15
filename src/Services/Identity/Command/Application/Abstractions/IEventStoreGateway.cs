using Domain.Abstractions.Aggregates;

namespace Application.Abstractions;

public interface IEventStoreGateway
{
    Task AppendAsync<TId>(IAggregateRoot<TId> aggregate, CancellationToken cancellationToken)
        where TId : struct;

    Task<IAggregateRoot<TId>> LoadAsync<TId>(TId aggregateId, CancellationToken cancellationToken)
        where TId : struct;
}