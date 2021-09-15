using Application.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections;
using Infrastructure.EventSourcing.Catalogs.Projections.Contexts;

namespace Infrastructure.EventSourcing.Catalogs.Projections
{
    public class CatalogProjectionsRepository : ProjectionsRepository, ICatalogProjectionsRepository
    {
        public CatalogProjectionsRepository(IMongoDbContext context)
            : base(context) { }
    }
}