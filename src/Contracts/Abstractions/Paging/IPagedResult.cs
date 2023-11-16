using MassTransit;

namespace Contracts.Abstractions.Paging;

[ExcludeFromTopology]
public interface IPagedResult<out TProjection>
{
    IReadOnlyCollection<TProjection> Items { get; }
    Page Page { get; }
}