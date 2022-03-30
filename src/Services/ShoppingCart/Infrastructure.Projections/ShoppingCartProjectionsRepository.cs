using Application.EventSourcing.Projections;
using Infrastructure.Projections.Abstractions;
using Infrastructure.Projections.Abstractions.Contexts;

namespace Infrastructure.Projections;

public class ShoppingCartProjectionsRepository : ProjectionsRepository, IShoppingCartProjectionsRepository
{
    public ShoppingCartProjectionsRepository(IMongoDbContext context)
        : base(context) { }
}