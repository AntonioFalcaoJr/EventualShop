using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections;

public interface IShoppingCartProjectionsService : IProjectionsService
{
    Task<CartDetailsProjection> GetCartDetailsAsync(Guid userId, CancellationToken cancellationToken);
}