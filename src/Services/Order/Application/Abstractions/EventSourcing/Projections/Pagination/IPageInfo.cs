namespace Application.Abstractions.EventSourcing.Projections.Pagination
{
    public interface IPageInfo
    {
        int Current { get; }
        int Size { get; }
        bool HasPrevious { get; }
        bool HasNext { get; }
    }
}