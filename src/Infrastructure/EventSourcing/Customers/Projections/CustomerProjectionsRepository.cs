using Application.EventSourcing.Customers.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections;
using Infrastructure.EventSourcing.Customers.Projections.Contexts;

namespace Infrastructure.EventSourcing.Customers.Projections
{
    public class CustomerProjectionsRepository : ProjectionsRepository, ICustomerProjectionsRepository
    {
        public CustomerProjectionsRepository(IMongoDbContext context)
            : base(context) { }
    }
}