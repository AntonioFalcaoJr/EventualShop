namespace Application.Abstractions.EventSourcing.Projections.Pagination
{
    public interface IPaging
    {
        int Limit { get; }
        int Offset { get; }
    }
}