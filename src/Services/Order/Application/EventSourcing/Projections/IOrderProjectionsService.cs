using System.Linq.Expressions;
using Application.Abstractions.EventSourcing.Projections;
using Application.Abstractions.EventSourcing.Projections.Pagination;

namespace Application.EventSourcing.Projections;

public interface IOrderProjectionsService : IProjectionsService
{
    Task<IPagedResult<OrderDetailsProjection>> GetOrderDetailsWithPaginationAsync(Paging paging, Expression<Func<OrderDetailsProjection, bool>> predicate, CancellationToken cancellationToken);
    Task<OrderDetailsProjection> GetOrderDetailsAsync(Guid orderId, CancellationToken cancellationToken);
}