using System.Collections.Generic;
using MassTransit;

namespace ECommerce.Abstractions.Messages.Queries.Responses;

[ExcludeFromTopology]
public abstract record ResponseListResult<T> : Message, IResponse
{
    public IEnumerable<T> Items { get; init; }
}