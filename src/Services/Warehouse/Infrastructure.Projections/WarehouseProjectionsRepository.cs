using Application.EventSourcing.Projections;
using Infrastructure.Projections.Abstractions;
using Infrastructure.Projections.Abstractions.Contexts;

namespace Infrastructure.Projections;

public class WarehouseProjectionsRepository : ProjectionsRepository, IWarehouseProjectionsRepository
{
    public WarehouseProjectionsRepository(IMongoDbContext context)
        : base(context) { }
}