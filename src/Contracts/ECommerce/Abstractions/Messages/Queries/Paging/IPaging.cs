using MassTransit;

namespace ECommerce.Abstractions.Messages.Queries.Paging;

[ExcludeFromTopology]
public interface IPaging
{
    int Limit { get; }
    int Offset { get; }
}