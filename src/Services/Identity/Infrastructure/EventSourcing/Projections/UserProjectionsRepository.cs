using Application.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;

namespace Infrastructure.EventSourcing.Projections;

public class UserProjectionsRepository : ProjectionsRepository, IUserProjectionsRepository
{
    public UserProjectionsRepository(IMongoDbContext context)
        : base(context) { }
}