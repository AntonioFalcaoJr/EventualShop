using Application.EventSourcing.Projections;
using Infrastructure.Projections.Abstractions;
using Infrastructure.Projections.Abstractions.Contexts;

namespace Infrastructure.Projections;

public class UserProjectionsRepository : ProjectionsRepository, IUserProjectionsRepository
{
    public UserProjectionsRepository(IMongoDbContext context)
        : base(context) { }
}