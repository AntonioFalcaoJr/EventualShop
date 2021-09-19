using Application.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections;
using Infrastructure.EventSourcing.Projections.Contexts;

namespace Infrastructure.EventSourcing.Projections
{
    public class CatalogProjectionsRepository : ProjectionsRepository, ICatalogProjectionsRepository
    {
        public CatalogProjectionsRepository(IMongoDbContext context)
            : base(context) { }
    }
}