using Application.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;

namespace Infrastructure.EventSourcing.Projections;

public class WarehouseProjectionsRepository : ProjectionsRepository, IWarehouseProjectionsRepository
{
    public WarehouseProjectionsRepository(IMongoDbContext context)
        : base(context) { }
}