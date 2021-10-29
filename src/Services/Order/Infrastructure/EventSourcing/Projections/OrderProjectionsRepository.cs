using Application.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;

namespace Infrastructure.EventSourcing.Projections;

public class OrderProjectionsRepository : ProjectionsRepository, IOrderProjectionsRepository
{
    public OrderProjectionsRepository(IMongoDbContext context)
        : base(context) { }
}