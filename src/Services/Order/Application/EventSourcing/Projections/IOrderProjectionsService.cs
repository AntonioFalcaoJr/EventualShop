using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;

namespace Application.EventSourcing.Projections;

public interface IOrderProjectionsService
{
    Task<IPagedResult<OrderDetailsProjection>> GetOrderDetailsWithPaginationAsync(Paging paging, Expression<Func<OrderDetailsProjection, bool>> predicate, CancellationToken cancellationToken);
    Task ProjectOrderDetailsAsync(OrderDetailsProjection orderDetailsProjection, CancellationToken cancellationToken);
    Task<OrderDetailsProjection> GetOrderDetailsAsync(Guid orderId, CancellationToken cancellationToken);
}