using Application.EventSourcing.Projections;
using Infrastructure.Projections.Abstractions;
using Infrastructure.Projections.Abstractions.Contexts;

namespace Infrastructure.Projections;

public class CatalogProjectionsRepository : ProjectionsRepository, ICatalogProjectionsRepository
{
    public CatalogProjectionsRepository(IMongoDbContext context)
        : base(context) { }
}