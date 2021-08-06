using Application.Abstractions.EventSourcing.Projections.Pagination;

namespace Application.Abstractions.UseCases
{
    public interface IQueryPaging : IQuery, IPaging { }
}