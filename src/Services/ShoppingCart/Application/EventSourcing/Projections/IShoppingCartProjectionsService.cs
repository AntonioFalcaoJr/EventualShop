using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections;

public interface IShoppingCartProjectionsService : IProjectionsService
{
    Task<CartDetailsProjection> GetCartDetailsAsync(Guid userId, CancellationToken cancellationToken);
    Task DiscardCartAsync(Guid userId, CancellationToken cancellationToken);
}