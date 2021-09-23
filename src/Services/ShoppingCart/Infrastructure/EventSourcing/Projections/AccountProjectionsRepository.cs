using Application.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;

namespace Infrastructure.EventSourcing.Projections
{
    public class AccountProjectionsRepository : ProjectionsRepository, IAccountProjectionsRepository
    {
        public AccountProjectionsRepository(IMongoDbContext context)
            : base(context) { }
    }
}