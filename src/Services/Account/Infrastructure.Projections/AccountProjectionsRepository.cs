using Application.EventSourcing.Projections;
using Infrastructure.Projections.Abstractions;
using Infrastructure.Projections.Abstractions.Contexts;

namespace Infrastructure.Projections;

public class AccountProjectionsRepository : ProjectionsRepository, IAccountProjectionsRepository
{
    public AccountProjectionsRepository(IMongoDbContext context)
        : base(context) { }
}