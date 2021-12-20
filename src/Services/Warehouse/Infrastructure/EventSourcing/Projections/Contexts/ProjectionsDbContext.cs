using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.EventSourcing.Projections.Contexts;

public class ProjectionsDbContext : MongoDbContext
{
    public ProjectionsDbContext(IConfiguration configuration)
        : base(configuration) { }
}