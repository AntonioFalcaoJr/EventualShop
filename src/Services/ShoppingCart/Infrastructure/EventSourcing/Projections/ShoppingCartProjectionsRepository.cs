using Application.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;

namespace Infrastructure.EventSourcing.Projections
{
    public class ShoppingCartProjectionsRepository : ProjectionsRepository, IShoppingCartProjectionsRepository
    {
        public ShoppingCartProjectionsRepository(IMongoDbContext context)
            : base(context) { }
    }
}