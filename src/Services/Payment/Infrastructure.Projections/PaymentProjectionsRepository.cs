using Application.EventSourcing.Projections;
using Infrastructure.Projections.Abstractions;
using Infrastructure.Projections.Abstractions.Contexts;

namespace Infrastructure.Projections;

public class PaymentProjectionsRepository : ProjectionsRepository, IPaymentProjectionsRepository
{
    public PaymentProjectionsRepository(IMongoDbContext context)
        : base(context) { }
}