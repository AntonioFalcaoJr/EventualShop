using Application.EventSourcing.Projections;
using Infrastructure.Projections.Abstractions;
using Infrastructure.Projections.Abstractions.Contexts;

namespace Infrastructure.Projections;

public class OrderProjectionsRepository : ProjectionsRepository, IOrderProjectionsRepository
{
    public OrderProjectionsRepository(IMongoDbContext context)
        : base(context) { }
}