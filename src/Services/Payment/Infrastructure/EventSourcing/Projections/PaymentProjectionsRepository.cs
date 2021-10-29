using Application.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;

namespace Infrastructure.EventSourcing.Projections;

public class PaymentProjectionsRepository : ProjectionsRepository, IPaymentProjectionsRepository
{
    public PaymentProjectionsRepository(IMongoDbContext context)
        : base(context) { }
}