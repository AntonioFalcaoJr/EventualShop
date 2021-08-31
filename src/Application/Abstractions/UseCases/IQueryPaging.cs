using Application.Abstractions.EventSourcing.Projections.Pagination;
using MassTransit.Topology;

namespace Application.Abstractions.UseCases
{
    [ExcludeFromTopology]
    public interface IQueryPaging : IQuery, IPaging { }
}