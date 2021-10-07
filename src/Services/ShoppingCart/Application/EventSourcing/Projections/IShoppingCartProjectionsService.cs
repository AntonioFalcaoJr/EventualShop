using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.EventSourcing.Projections
{
    public interface IShoppingCartProjectionsService
    {
        Task ProjectCartDetailsAsync(CartDetailsProjection cartDetails, CancellationToken cancellationToken);
        Task UpdateCartDetailsAsync(CartDetailsProjection cartDetails, CancellationToken cancellationToken);
        Task<CartDetailsProjection> GetCartDetailsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}