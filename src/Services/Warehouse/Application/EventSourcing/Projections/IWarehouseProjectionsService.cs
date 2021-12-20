using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections;

public interface IWarehouseProjectionsService : IProjectionsService
{
    Task<ProductDetailsProjection> GetProductDetailsAsync(Guid productId, CancellationToken cancellationToken);
}