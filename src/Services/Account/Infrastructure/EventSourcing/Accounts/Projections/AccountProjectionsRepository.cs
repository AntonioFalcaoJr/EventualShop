using Application.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections;
using Infrastructure.EventSourcing.Accounts.Projections.Contexts;

namespace Infrastructure.EventSourcing.Accounts.Projections
{
    public class AccountProjectionsRepository : ProjectionsRepository, IAccountProjectionsRepository
    {
        public AccountProjectionsRepository(IMongoDbContext context)
            : base(context) { }
    }
}