using MassTransit;

namespace Contracts.Abstractions.Paging;

[ExcludeFromTopology]
public interface IPagedResult<out TProjection>
{
    IReadOnlyList<TProjection> Items { get; }
    Page Page { get; }
}