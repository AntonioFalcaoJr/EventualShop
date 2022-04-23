using Infrastructure.Projections.Abstractions.Contexts;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Projections;

public class ProjectionDbContext : MongoDbContext
{
    public ProjectionDbContext(IConfiguration configuration)
        : base(configuration) { }
}