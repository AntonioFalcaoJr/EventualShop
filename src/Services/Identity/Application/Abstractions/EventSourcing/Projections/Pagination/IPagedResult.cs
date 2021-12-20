namespace Application.Abstractions.EventSourcing.Projections.Pagination;

public interface IPagedResult<out T>
{
    IEnumerable<T> Items { get; }
    IPageInfo PageInfo { get; }
}