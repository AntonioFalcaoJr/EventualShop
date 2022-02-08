using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;

namespace Application.EventSourcing.Projections;

public interface IOrderProjectionsService : IProjectionsService
{
    Task<IPagedResult<OrderDetailsProjection>> GetOrderDetailsWithPaginationAsync(IPaging paging, Expression<Func<OrderDetailsProjection, bool>> predicate, CancellationToken cancellationToken);
    Task<OrderDetailsProjection> GetOrderDetailsAsync(Guid orderId, CancellationToken cancellationToken);
}